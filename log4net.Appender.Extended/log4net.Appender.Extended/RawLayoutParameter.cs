using log4net.Core;
using log4net.Layout;

namespace log4net.Appender.Extended
{
    public class RawLayoutParameter
    {
        public string ParameterName { get; set; }

        public IRawLayout Layout { get; set; }

        public string Render(LoggingEvent loggingEvent)
        {
            var format = Layout.Format(loggingEvent) ?? "[Null]";
            return format.ToString();
        }
    }
}