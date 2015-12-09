using System.Collections.Generic;
using log4net.Appender.Extended;

namespace log4net.Appender.SplunkAppenders
{
    public class SplunkEntry
    {
        public SplunkEntry() { Variables = new List<KeyValuePair<string, string>>(100); }

        public SplunkEntry(ExtendedLoggingEvent extendedLoggingEvent)
        {
            Application = extendedLoggingEvent.Application;
            Machine = extendedLoggingEvent.Machine;
            Message = extendedLoggingEvent.Message;
            StackTrace = extendedLoggingEvent.StackTrace;
            LogLevel = extendedLoggingEvent.LoggingEvent.Level.Name;
            Variables = extendedLoggingEvent.Variables;
        }

        public string Application { get; set; }

        public string Machine { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public string LogLevel { get; set; }

        public List<KeyValuePair<string, string>> Variables { get; set; }
    }
}