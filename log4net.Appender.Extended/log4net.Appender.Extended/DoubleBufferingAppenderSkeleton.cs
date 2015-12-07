using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using log4net.Core;
using Timer = System.Timers.Timer;

namespace log4net.Appender.Extended
{
    public abstract class DoubleBufferingAppenderSkeleton : BufferingAppenderSkeleton
    {
        // ReSharper disable once StaticMemberInGenericType
        private static readonly Timer BufferTimer = new Timer(1000);

        protected DoubleBufferingAppenderSkeleton()
        {
            Parameters = new List<LayoutParameter>(10);
            BufferTimer.Elapsed += BufferTimerElapsed;
            BufferTimer.Start();
            BufferSize = 1;
            Lossy = false;
            // ReSharper disable once VirtualMemberCallInContructor
            ActivateOptions();
        }

        private int _bufferTimeCounter;
        private bool _inPeriod;

        protected void BufferTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (_inPeriod) return;
            try
            {
                _inPeriod = true;
                _bufferTimeCounter++;
                if (_bufferTimeCounter > TimeThreshold)
                {
                    _bufferTimeCounter = 0;
                    Send();
                }
                else
                {
                    if (_eventRequests.Count >= MaxBufferSize)
                    {
                        Send();
                    }
                }
            }
            finally
            {
                _inPeriod = false;
            }
        }

        private int _timeThreshold = 60;

        /// <summary>
        ///     Time threshold as seconds default is 60
        /// </summary>
        public int TimeThreshold
        {
            get { return _timeThreshold; }
            set { _timeThreshold = value; }
        }

        private int _maxBufferSize = 100;

        /// <summary>
        ///     Buffer threshold as count default is 100
        /// </summary>
        public int MaxBufferSize
        {
            get { return _maxBufferSize; }
            set { _maxBufferSize = value; }
        }

        /// <summary>
        ///     Layout parameters for custom metrics
        /// </summary>
        public List<LayoutParameter> Parameters { get; set; }

        public void AddParameter(LayoutParameter parameter) { Parameters.Add(parameter); }


        private readonly ConcurrentBag<ExtendedLoggingEvent> _eventRequests = new ConcurrentBag<ExtendedLoggingEvent>();

        protected override void SendBuffer(LoggingEvent[] events)
        {
            var requestList = new List<LoggingEvent>(BufferSize);
            requestList.AddRange(events);
            foreach (var logEventRequest in requestList)
            {
                _eventRequests.Add(ConvertLoggingEvent(logEventRequest, Parameters));
            }
        }

        protected virtual ExtendedLoggingEvent ConvertLoggingEvent(LoggingEvent loggingEvent, IList<LayoutParameter> parameters)
        {
            var extendedLoggingEvent = Utility.ConvertLoggingEvent(loggingEvent, parameters);
            return extendedLoggingEvent;
        }

        private bool _sentInPeriod;

        protected void Send()
        {
            if (_sentInPeriod) return;
            try
            {
                _sentInPeriod = true;
                if (_eventRequests.IsEmpty)
                {
                    _sentInPeriod = false;
                    return;
                }
                var requestList = new List<ExtendedLoggingEvent>(MaxBufferSize);
                while (!_eventRequests.IsEmpty)
                {
                    ExtendedLoggingEvent extendedLoggingEvent;
                    if (_eventRequests.TryTake(out extendedLoggingEvent))
                    {
                        requestList.Add(extendedLoggingEvent);
                    }
                }
                BulkSend(requestList);
            }
            finally
            {
                _sentInPeriod = false;
            }
        }

        protected abstract void BulkSend(IEnumerable<ExtendedLoggingEvent> customLoggingEvents);

        protected override void OnClose()
        {
            base.OnClose();
            BufferTimer.Stop();
            Thread.Sleep(1000);
            Send();
        }
    }
}