using System.Collections.Generic;
using log4net.Core;

namespace log4net.Appender.Extended
{
    public static class Utility
    {
        public static ExtendedLoggingEvent ConvertLoggingEvent(LoggingEvent loggingEvent, IList<LayoutParameter> parameters)
        {
            var extendedLoggingEvent = new ExtendedLoggingEvent(loggingEvent);
            foreach (var layoutParameter in parameters)
            {
                extendedLoggingEvent.EventParameters.Add(new ExtendedLoggingEventParameter(layoutParameter.ParameterName, layoutParameter.Render(loggingEvent)));
            }
            return extendedLoggingEvent;
        }
    }
}
