using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Framing;
using Ulak.Contract;
using Ulak.Implementation.Seralizers;

namespace Ulak.Test
{
    [TestClass]
    public class UlakTests
    {
        private string endPoint = "";

        private string exchangeName = "TestExchange";
        private string routingKey = "TestRoute";
        private IMessageSerializer serializer = new JsonNetSerializer();
        private ConnectionFactory factory = new ConnectionFactory
        {
            UserName = "guest",
            Password = "guest",
            VirtualHost = "/",
            HostName = "localhost",
            Port = 5672
        };

        string queue1 = "queue1";
        string queue2 = "queue2";
        

        [TestMethod]
        public void Check_RabbitMQ_Server()
        {
            //SetupExchangeAndQueues();

            var message = new NotificationMessage()
            {
                Source = "Test",
                Target = "Test",
                Message = "10"
            };
            var bytez = serializer.Serialize(message);

            using (IConnection conn = factory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    // SetupExchangeAndQueues
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout);

                    channel.QueueDeclare(queue1, true, false, false, null);
                    channel.QueueDeclare(queue2, true, false, false, null);

                    channel.QueueBind(queue1, exchangeName, routingKey, null);
                    channel.QueueBind(queue2, exchangeName, routingKey, null);
                    channel.BasicPublish(exchangeName, routingKey, null, bytez);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(queue2, true, consumer);

                    Trace.WriteLine("Waiting for messages");

                    bool stopBreak = false;

                    while (!stopBreak)
                    {
                        var ea = consumer.Queue.Dequeue();
                        var body = ea.Body;
                        var receivedMessage = serializer.Deserialie<NotificationMessage>(body);
                        Trace.WriteLine(string.Format("Message Received: {0} From: {1}, To: {2}", receivedMessage.Message, receivedMessage.Source, receivedMessage.Target));
                        var t = int.Parse(receivedMessage.Message);
                        if (t == 0) stopBreak = true;
                        else t--;
                        receivedMessage.Message = t.ToString(CultureInfo.InvariantCulture);
                        var newBytez = serializer.Serialize(receivedMessage);
                        channel.BasicPublish(exchangeName, routingKey, null, newBytez);
                        
                    }
                }
            }
        }

        private void SetupExchangeAndQueues()
        {
            using (IConnection conn = factory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout);

                    channel.QueueDeclare(queue1, true, false, false, null);
                    channel.QueueDeclare(queue2, true, false, false, null);

                    channel.QueueBind(queue1, exchangeName, routingKey, null);
                    channel.QueueBind(queue2, exchangeName, routingKey, null);
                }
            }
        }

    }
}
