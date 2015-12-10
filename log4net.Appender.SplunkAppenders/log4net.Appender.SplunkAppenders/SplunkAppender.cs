using log4net.Appender.Extended;

namespace log4net.Appender.SplunkAppenders
{
    public class SplunkAppender : ExtendedAppenderSkeleton
    {
        public bool UseFreshSession { get; set; }

        public string SplunkHost { get; set; }

        private int _splunkPort = 8089;
        public int SplunkPort
        {
            get { return _splunkPort; }
            set { _splunkPort = value; }
        }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string IndexName { get; set; }

        public bool Async { get; set; }

        private int _sessionTimeout = 55;
        

        /// <summary>
        ///     Session timeout as minutes. (Default splunk session timeout is 1 hour, you should give timeout value less then
        ///     splunkd session timeout). Default is 55 minutes.
        /// </summary>
        public int SessionTimeout
        {
            get { return _sessionTimeout; }
            set { _sessionTimeout = value; }
        }

        protected override void AppendExtended(ExtendedLoggingEvent extendedLoggingEvent)
        {
            var splunkEntry = new SplunkEntry(extendedLoggingEvent);
            var data = Utility.Serialize(splunkEntry);
            Send(data);
        }

        protected virtual void Send(string data)
        {
            if (Async)
            {
#pragma warning disable 4014
                SplunkContainer.LogAsync(data, SplunkHost, IndexName, UserName, Password, SplunkPort, ErrorHandler, UseFreshSession, SessionTimeout);
#pragma warning restore 4014
            }
            else
            {
                SplunkContainer.Log(data, SplunkHost, IndexName, UserName, Password, SplunkPort, ErrorHandler, UseFreshSession, SessionTimeout);
            }
        }
    }
}