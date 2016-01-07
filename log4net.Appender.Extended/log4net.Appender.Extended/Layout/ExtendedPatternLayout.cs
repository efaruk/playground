 using System.IO;
 using log4net.Core;
 using log4net.Layout;

namespace log4net.Appender.Extended.Layout
{
    public class ExtendedPatternLayout: PatternLayout
    {
        public ExtendedPatternLayout(): this(DefaultConversionPattern)
        {
        }

        public ExtendedPatternLayout(string pattern) : base(pattern)
        {
            AddConverter("extended-aspnet-cache", typeof(ExtendedAspNetCachePatternConverter));
            AddConverter("extended-aspnet-context", typeof(ExtendedAspNetContextPatternConverter));
            AddConverter("extended-aspnet-request", typeof(ExtendedAspNetRequestPatternConverter));
            AddConverter("extended-aspnet-session", typeof(ExtendedAspNetSessionPatternConverter));
        }

        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            if (LevelMin <= loggingEvent.Level && LevelMax >= loggingEvent.Level)
            {
                base.Format(writer, loggingEvent);
            }
        }

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
        
    }
}
