using System.Collections.Generic;
using log4net.Core;

namespace log4net.Appender.Extended
{
    public class ExtendedAppenderSkeleton : AppenderSkeleton
    {
        /// <summary>
        ///     Layout parameters for custom metrics
        /// </summary>
        public List<LayoutParameter> Parameters { get; set; }

        public void AddParameter(LayoutParameter parameter) { Parameters.Add(parameter); }

        protected override void Append(LoggingEvent loggingEvent) { var extendedLoggingEvent = ConvertLoggingEvent(loggingEvent, Parameters); }

        protected virtual ExtendedLoggingEvent ConvertLoggingEvent(LoggingEvent loggingEvent, IList<LayoutParameter> parameters)
        {
            var extendedLoggingEvent = Utility.ConvertLoggingEvent(loggingEvent, parameters);
            return extendedLoggingEvent;
        }
    }
}