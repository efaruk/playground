using log4net.Core;
using log4net.Layout;
using log4net.Util;

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

        public bool OmitNull { get; set; }

        public string ParameterName { get; set; }

        public IRawLayout Layout { get; set; }

        public string Render(LoggingEvent loggingEvent)
        {
            var format = Layout.Format(loggingEvent) ?? SystemInfo.NullText;
            return format.ToString();
        }
    }
}