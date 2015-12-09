using log4net.Appender.Extended;

namespace log4net.Appender.SplunkAppenders
{
    public class SplunkEntry
    {

        public SplunkEntry() {
            
        }

        public SplunkEntry(ExtendedLoggingEvent extendedLoggingEvent)
        {
            Application = extendedLoggingEvent.Application;
            Machine = extendedLoggingEvent.Machine;
            Message = extendedLoggingEvent.Message;
            StackTrace = extendedLoggingEvent.StackTrace;
            LogLevel = extendedLoggingEvent.LoggingEvent.Level.Name;
        }

        public string Application { get; set; }

        public string Machine { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public string LogLevel { get; set; }

    }
}
