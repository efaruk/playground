using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using log4net.Core;

namespace log4net.Appender.Extended
{
    public class ExtendedLoggingEvent
    {
        public ExtendedLoggingEvent() {
            EventParameters = new List<ExtendedLoggingEventParameter>(10);
            CustomParameters = new List<ExtendedLoggingEventParameter>(10);
        }

        public ExtendedLoggingEvent(LoggingEvent loggingEvent) : this() { LoggingEvent = loggingEvent; }

        public ExtendedLoggingEvent(LoggingEvent loggingEvent, IList<ExtendedLoggingEventParameter> eventParameters, IList<ExtendedLoggingEventParameter> customParameters)
        {
            LoggingEvent = loggingEvent;
            EventParameters = eventParameters;
            CustomParameters = customParameters;
        }

        public LoggingEvent LoggingEvent { get; set; }

        public IList<ExtendedLoggingEventParameter> EventParameters { get; set; }

        public IList<ExtendedLoggingEventParameter> CustomParameters { get; set; }
    }

    public class ExtendedLoggingEventParameter
    {
        public ExtendedLoggingEventParameter() {
            
        }

        public ExtendedLoggingEventParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }

        public string Value { get; set; }
         
    }
}
