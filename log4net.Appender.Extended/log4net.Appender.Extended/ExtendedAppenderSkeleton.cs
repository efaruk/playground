using System.Collections.Generic;
using log4net.Core;

namespace log4net.Appender.Extended
{
    public class ExtendedAppenderSkeleton : AppenderSkeleton
    {
        private List<LayoutParameter> _parameters = new List<LayoutParameter>(10);

        /// <summary>
        ///     Layout parameters for custom metrics
        /// </summary>
        public List<LayoutParameter> Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        public void AddParameter(LayoutParameter parameter) { Parameters.Add(parameter); }

        protected override void Append(LoggingEvent loggingEvent)
        {
            var extendedLoggingEvent = ConvertLoggingEvent(loggingEvent, Parameters);
            AppendExtended(extendedLoggingEvent);
        }

        protected virtual void AppendExtended(ExtendedLoggingEvent extendedLoggingEvent) { }

        protected virtual ExtendedLoggingEvent ConvertLoggingEvent(LoggingEvent loggingEvent, IList<LayoutParameter> parameters)
        {
            var extendedLoggingEvent = Utility.ConvertLoggingEvent(loggingEvent, parameters);
            return extendedLoggingEvent;
        }
    }
}