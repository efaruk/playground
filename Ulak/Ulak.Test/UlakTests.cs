using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Ulak.Contract;
using Ulak.Implementation.Seralizers;

namespace Ulak.Test
{
    [TestClass]
    public class UlakTests
    {
        private const string ExchangeName = "TestExchange";
        private const string QueuePrefix = "TestQueue";
        private const string RoutingKey = "";
        private readonly IMessageSerializer _serializer = new JsonNetSerializer();
        private readonly ConnectionFactory factory = new ConnectionFactory
        {
            UserName = "guest",
            Password = "guest",
            VirtualHost = "/",
            HostName = "localhost",
            Port = 5672
        };


        private const int MaxQueueCount = 10;
        List<string> queueList = new List<string>(MaxQueueCount);
        

        [TestMethod]
        public void TestRabbitMqBehavior()
        {
            SetupQueueList(10);
            SetupExchangeAndQueues();
            PublishMessages(100);
            Parallel.ForEach(queueList, ConsumeMessages);         
            Trace.WriteLine("All queues consumed...");
        }

        private void SetupQueueList(int queueCount)
        {
            queueList.Clear();
            for (int i = 0; i < queueCount; i++)
            {
                var queueName = QueuePrefix + i;
                queueList.Add(queueName);
            }
        }

        private void SetupExchangeAndQueues()
        {
            Trace.WriteLine("Setting Up RabbitMQ Exchange and Queues");
            using (IConnection conn = factory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    channel.ExchangeDeclare(ExchangeName, ExchangeType.Fanout, true, false, null);

                    foreach (var queueName in queueList)
                    {
                        channel.QueueDeclare(queueName, true, false, false, null);
                        channel.QueueBind(queueName, ExchangeName, RoutingKey, null);
                    }
                }
            }

            Trace.WriteLine("RabbitMQ setup completed");
        }

        private void PublishMessages(int messageCount)
        {
            var message = new NotificationMessage()
            {
                Source = "Test",
                Target = "Test"
            };

            Trace.WriteLine(string.Format("Publishing {0} messages", messageCount));

            using (IConnection conn = factory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    channel.ConfirmSelect();
                    
                    IBasicProperties messageProperties = channel.CreateBasicProperties();
                    messageProperties.SetPersistent(true);
                    

                    for (int i = messageCount; i > 0; i--)
                    {
                        messageProperties.CorrelationId = Guid.NewGuid().ToString();
                        message.Message = i.ToString(CultureInfo.InvariantCulture);
                        var bytez = _serializer.Serialize(message);
                        channel.BasicPublish(ExchangeName, RoutingKey, messageProperties, bytez);
                    }
                    channel.WaitForConfirmsOrDie(DateTime.Now.AddMinutes(1).TimeOfDay);
                }
            }
            Trace.WriteLine(string.Format("{0} messages puplished", messageCount));
        }

        private void ConsumeMessages(string queueName)
        {
            using (IConnection conn = factory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(queueName, false, consumer);
                    Trace.WriteLine(string.Format("Waiting for messages from: {0}", queueName));
                    
                    while (true)
                    {
                        BasicDeliverEventArgs ea = null;
                        try
                        {
                            ea = consumer.Queue.Dequeue();
                        }
                        catch (EndOfStreamException endOfStreamException)
                        {
                            Trace.WriteLine(endOfStreamException);
                        }
                        if (ea == null) break;
                        var body = ea.Body;
                        var receivedMessage = _serializer.Deserialie<NotificationMessage>(body);
                        Trace.WriteLine(
                            string.Format(
                                "Message Received: {0} From: {1}, To: {2} on Thread: {3} with CorrelationId: {4} and DeliveryTag: {5}",
                                receivedMessage.Message, receivedMessage.Source, receivedMessage.Target,
                                Thread.CurrentThread.ManagedThreadId, ea.BasicProperties.CorrelationId, ea.DeliveryTag));
                        Thread.Sleep(300);
                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                }
            }
        }

    }
}
