using System;
using log4net.Appender.Extended;
using log4net.Core;

namespace log4net.Appender.SplunkAppenders
{
    public class SplunkAppender : ExtendedAppenderSkeleton
    {
        private int _sessionTimeout = 55;

        private bool UseFreshSession { get; set; }

        public string SplunkUrl { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string IndexName { get; set; }

        public bool Async { get; set; }

        /// <summary>
        ///     Session timeout as minutes. (Default splunk session timeout is 1 hour, you should give timeout value less then splunkd session timeout).
        /// </summary>
        public int SessionTimeout
        {
            get { return _sessionTimeout; }
            set { _sessionTimeout = value; }
        }

        protected override void AppendExtended(ExtendedLoggingEvent extendedLoggingEvent)
        {
            var data = Utility.Serialize(extendedLoggingEvent);
            if (Async)
            {
                SplunkContainer.LogAsync(data, SplunkUrl, IndexName, UserName, Password, ErrorHandler, false, SessionTimeout);
            }
            else
            {
                SplunkContainer.Log(data, SplunkUrl, IndexName, UserName, Password, ErrorHandler, false, SessionTimeout);
            }
        }
    }
}
