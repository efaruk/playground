using System;
using System.Collections.Generic;
using log4net.Appender.Extended.Layout;
using log4net.Core;

namespace log4net.Appender.Extended
{
    public class ExtendedLoggingEvent
    {
        private LoggingEvent _loggingEvent;

        public ExtendedLoggingEvent()
        {
            EventParameters = new List<RenderedLayoutParameter>(10);
            Variables = new List<KeyValuePair<string, string>>(100);
            Machine = Environment.MachineName;
        }

        public ExtendedLoggingEvent(LoggingEvent loggingEvent) : this() { LoggingEvent = loggingEvent; }

        public ExtendedLoggingEvent(LoggingEvent loggingEvent, List<RenderedLayoutParameter> eventParameters, List<KeyValuePair<string, string>> variables) : this()
        {
            LoggingEvent = loggingEvent;
            EventParameters = eventParameters;
            Variables = variables;
        }

        public LoggingEvent LoggingEvent
        {
            get { return _loggingEvent; }
            set
            {
                if (value != null && value.ExceptionObject != null)
                {
                    Exception = new SerializableException(value.ExceptionObject);
                }
                _loggingEvent = value;
            }
        }

        public string Application { get; set; }

        public string Machine { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public List<RenderedLayoutParameter> EventParameters { get; set; }

        public List<KeyValuePair<string, string>> Variables { get; set; }

        public SerializableException Exception { get; set; }
    }
}