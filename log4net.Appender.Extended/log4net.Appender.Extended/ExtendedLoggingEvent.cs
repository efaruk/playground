using System.Collections.Generic;
using log4net.Core;

namespace log4net.Appender.Extended
{
    public class ExtendedLoggingEvent
    {
        private LoggingEvent _loggingEvent;

        public ExtendedLoggingEvent()
        {
            EventParameters = new List<ExtendedLoggingEventParameter>(10);
            CustomParameters = new List<ExtendedLoggingEventParameter>(10);
        }

        public ExtendedLoggingEvent(LoggingEvent loggingEvent) : this()
        {
            LoggingEvent = loggingEvent;
        }

        public ExtendedLoggingEvent(LoggingEvent loggingEvent, IList<ExtendedLoggingEventParameter> eventParameters, IList<ExtendedLoggingEventParameter> customParameters)
        {
            LoggingEvent = loggingEvent;
            EventParameters = eventParameters;
            CustomParameters = customParameters;
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

        public IList<ExtendedLoggingEventParameter> EventParameters { get; set; }

        public IList<ExtendedLoggingEventParameter> CustomParameters { get; set; }

        public SerializableException Exception { get; set; }
    }

    public class ExtendedLoggingEventParameter
    {
        public ExtendedLoggingEventParameter() { }

        public ExtendedLoggingEventParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}