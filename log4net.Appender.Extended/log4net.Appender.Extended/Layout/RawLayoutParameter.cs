using log4net.Core;
using log4net.Layout;

namespace log4net.Appender.Extended.Layout
{
    public class RawLayoutParameter
    {
        private Level _levelMax = Level.Off;
        private Level _levelMin = Level.All;

        public Level LevelMin
        {
            get { return _levelMin; }
            set { _levelMin = value; }
        }

        public Level LevelMax
        {
            get { return _levelMax; }
            set { _levelMax = value; }
        }

        public string ParameterName { get; set; }

        public IRawLayout Layout { get; set; }

        public string Render(LoggingEvent loggingEvent)
        {
            var format = Layout.Format(loggingEvent) ?? "[Null]";
            return format.ToString();
        }
    }
}