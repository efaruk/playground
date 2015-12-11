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
        
    }
}
