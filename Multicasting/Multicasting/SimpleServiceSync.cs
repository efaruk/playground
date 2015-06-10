using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Timers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Timer = System.Timers.Timer;

namespace Multicasting
{

    public delegate void FailoverDelegate(bool isMaster);

    public static class SimpleServiceSync
    {

        #region Fields

        private const int HeartBeatInverval = 1000;
        private static int _portNumber = 5000;
        private static Timer _syncTimer = new Timer(HeartBeatInverval);
        private static int _failoverInterval = 60;
        private static int _failoverIntervalCounter;
        private static bool _isMaster;
        private static string _machineName;
        private static string _serviceName;
        private static MulticastSender _sender;
        private static MulticastReceiver _receiver;
        private static bool _running;
        private static int _maxHandshakeRetry = 10;
        private static bool _handshaking;
        private static bool _abortHandshake;
        private static Random _random = new Random();
        private static int _randomMax = 1000;
        private static int _luck;
        private static ServiceSyncMasterSlaveHandshakeMessage _lastReceivedHandshakeMessage;
        private static ServiceSyncMasterSlaveMessage _lastReceivedSyncMessage;
        private static ITraceWriter _traceWriter = new StandartTraceWriter();

        #endregion

        static SimpleServiceSync()
        {
            _machineName = Environment.MachineName;
            var ass = Assembly.GetEntryAssembly();
            if (ass == null) ass = Assembly.GetCallingAssembly();
            _serviceName = ass.GetName().Name;
            _syncTimer.Elapsed += SyncTimerElapsed;
            _sender = new MulticastSender(_portNumber);
            _receiver = new MulticastReceiver(_portNumber);
            _receiver.MessageReceived += ReceiverOnMessageReceived;
        }

        public static void Start()
        {
            if (_running) return;
            _lastReceivedHandshakeMessage = null;
            _lastReceivedSyncMessage = null;
            _isMaster = false;
            _handshaking = false;
            _handshakeRetryCount = 0;
            _failoverIntervalCounter = 0;
            _running = true;
            //var task = new Task(() => _receiver.Start());
            //task.Start();
            _receiver.Start();
            _syncTimer.Start();
        }

        public static void Stop()
        {
            if (!_running) return;
            _running = false;
            _isMaster = false;
            _handshaking = false;
            _syncTimer.Stop();
            _receiver.Stop();
        }

        public static event FailoverDelegate OnFailover;

        /// <summary>
        /// Port Number for UDP BroadCasting
        /// </summary>
        public static int PortNumber
        {
            get
            {
                return _portNumber;
            }
            set
            {
                if (value <= 0 && value > 65535) return;
                _portNumber = value;
            }
        }
        
        /// <summary>
        /// Set fail over interval as seconds
        /// </summary>
        public static int FailoverInterval
        {
            get { return _failoverInterval; }
            set
            {
                if (value < 10) return;
                _failoverInterval = value;
            }
        }

        /// <summary>
        /// Service name for sync operation
        /// </summary>
        public static string ServiceName
        {
            get { return _serviceName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;
                _serviceName = value;
            }
        }

        /// <summary>
        /// Do not required normally...
        /// </summary>
        /// <param name="machineName"></param>
        public static string MachineName
        {
            get { return _machineName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;
                _machineName = value;
            }
        }

        /// <summary>
        /// Get or set current sync is master or not, by default false
        /// </summary>
        public static bool IsMaster { get { return _isMaster; } set { _isMaster = value; }}

        public static int MaxHandshakeRetry
        {
            get
            {
                return _maxHandshakeRetry;
            }
            set
            {
                if (value < 10) return;
                _maxHandshakeRetry = value;
            }
        }

        public static void SetTraceWriter(ITraceWriter traceWriter)
        {
            if (traceWriter == null) return;
            _traceWriter = traceWriter;
        }

        private static void SyncTimerElapsed(object sender, ElapsedEventArgs e)
        {
            WriteTrace("Heartbeat!!!");
            if (_handshaking) return;
            if (!_isMaster) _failoverIntervalCounter++;
            if (_failoverIntervalCounter > FailoverInterval)
            {
                if (_isMaster)
                {
                    _failoverIntervalCounter = 0;
                }
                else
                {
                    Handshake();
                }
            }
            BroadCastMasterSlave();
        }

        private static void ReceiverOnMessageReceived(string message)
        {
            if (!_running) return;
            // WriteTrace("#Not Processed Message : " + message);
            var t = CheckIncomingMessage(message);
            switch (t)
            {
                case SyncMessageType.Sync:
                    SyncMessageReceived(message);
                    break;
                case SyncMessageType.Handshake:
                    HandshakeMessageReceived(message);
                    break;
            }
        }

        private static object _handshakeLock = new object();
        private static void HandshakeMessageReceived(string message)
        {
            //if (!_handshaking) return;
            ServiceSyncMasterSlaveHandshakeMessage m = null;
            try
            {
                m = Deserialize<ServiceSyncMasterSlaveHandshakeMessage>(message);
            }
            catch (Exception e)
            {
                WriteTrace(string.Format("Exception: {0}{2} Trace: {1}", e.Message, e.StackTrace, Environment.NewLine));
            }
            if (m == null) return;
            if (m.ServiceName != ServiceName) return;
            if (m.ServiceName == ServiceName && m.MachineName != MachineName)
            {
                lock (_handshakeLock)
                {
                    m.ReceivedAt = DateTime.Now;
                    _lastReceivedHandshakeMessage = m;
                }
            }
        }

        private static object _syncLock = new object();
        private static void SyncMessageReceived(string message)
        {
            if (_handshaking) return;
            ServiceSyncMasterSlaveMessage m = null;
            try
            {
                m = Deserialize<ServiceSyncMasterSlaveMessage>(message);
            }
            catch (Exception e)
            {
                WriteTrace(string.Format("Exception: {0}{2} Trace: {1}", e.Message, e.StackTrace, Environment.NewLine));
            }
            if (m == null) return;
            if (m.ServiceName != ServiceName) return;
            if (m.ServiceName == ServiceName && m.MachineName != MachineName)
            {
                if (m.Master)
                {
                    _abortHandshake = true;
                    _handshaking = false;
                    if (_isMaster)
                    {
                        _isMaster = false;
                        RaiseFailover();
                    }
                    {
                        _failoverIntervalCounter = 0;
                    }
                }
                lock (_syncLock)
                {
                    m.ReceivedAt = DateTime.Now;
                    _lastReceivedSyncMessage = m;
                }
            }
        }

        private static SyncMessageType CheckIncomingMessage(string message)
        {
            var d = DeserializeAsDynamic(message);
            if (d == null) return SyncMessageType.Unkown;
            var mt = SyncMessageType.Unkown;
            try
            {
                mt = d.MessageType;
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch { }
            return mt;
        }

        private static void BroadCastMasterSlave()
        {
            var message = new ServiceSyncMasterSlaveMessage()
            {
                MachineName = _machineName,
                ServiceName = ServiceName,
                Master = _isMaster,
                SentOn = DateTime.Now
            };
            var msg = Serialize(message);
            _sender.SendMessage(msg);
        }

        private static int _handshakeRetryCount;
        private static void Handshake()
        {
            if (_handshaking) return;
            try
            {
                _abortHandshake = false;
                lock (_handshakeLock)
                {
                    _lastReceivedHandshakeMessage = null;
                }
                _handshaking = true;
                var diffMiliseconds = HeartBeatInverval * MaxHandshakeRetry * 10;
                _luck = 0;
                _handshakeRetryCount = 0;
                WriteTrace("Handshake started...");
                WishLuck();
                Thread.Sleep(HeartBeatInverval * 5);
                while (!_abortHandshake)
                {
                    WriteTrace(string.Format("Handshake retry: {0}", _handshakeRetryCount));
                    var message = new ServiceSyncMasterSlaveHandshakeMessage()
                    {
                        Luck = _luck,
                        MachineName = MachineName,
                        ServiceName = ServiceName,
                        SentOn = DateTime.Now
                    };
                    var msg = Serialize(message);
                    _sender.SendMessage(msg);
                    Thread.Sleep(HeartBeatInverval * 5);

                    if (_lastReceivedHandshakeMessage != null && _lastReceivedHandshakeMessage.SentOn > DateTime.Now.AddMilliseconds(-diffMiliseconds))
                    {
                        if (_lastReceivedHandshakeMessage.Luck == _luck)
                        {
                            WriteTrace(string.Format("CAN NOT Decide we are EQUAL (Received: {0} == Sended: {1})", _lastReceivedHandshakeMessage.Luck, _luck));
                            WishLuck();
                            continue;
                        }
                        if (_lastReceivedHandshakeMessage.Luck < _luck)
                        {
                            WriteTrace(string.Format("Decided as MASTER by Luck (Received: {0} < Sended: {1})", _lastReceivedHandshakeMessage.Luck, _luck));
                            _isMaster = true;
                            RaiseFailover();
                            break;
                        }
                        if (_lastReceivedHandshakeMessage.Luck > _luck)
                        {
                            WriteTrace(string.Format("Decided as SLAVE by Luck (Received: {0} > Sended: {1})", _lastReceivedHandshakeMessage.Luck, _luck));
                            _isMaster = false;
                            RaiseFailover();
                            break;
                        }
                    }
                    if (_handshakeRetryCount > _maxHandshakeRetry)
                    {
                        WriteTrace("Decided as MASTER by RETRY");
                        _isMaster = true;
                        RaiseFailover();
                        break;
                    }
                    _handshakeRetryCount++;
                }
            }
            finally
            {
                _handshaking = false;
            }
            WriteTrace("Handshake ended...");
        }

        private static void WishLuck()
        {
            _luck = _random.Next(1, _randomMax);
        }

        private static void RaiseFailover()
        {
            _failoverIntervalCounter = 0;
            if (OnFailover != null) OnFailover(_isMaster);
        }

        private static T Deserialize<T>(string message)
        {
            var result = JsonConvert.DeserializeObject<T>(message);
            return result;
        }

        private static string Serialize(object message)
        {
            var result = JsonConvert.SerializeObject(message);
            return result;
        }

        private static dynamic DeserializeAsDynamic(string message)
        {
            var result = JObject.Parse(message);
            return result;
        }

        private static void WriteTrace(string message)
        {
            _traceWriter.WriteLine(message);
        }

    }

    public class StandartTraceWriter : ITraceWriter
    {
        public void WriteLine(string message)
        {
            Trace.WriteLine(message);
        }
    }

    public interface ITraceWriter
    {
        void WriteLine(string message);
    }
}
