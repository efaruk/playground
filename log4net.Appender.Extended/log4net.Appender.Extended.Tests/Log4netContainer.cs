using System;
using System.Reflection;
using log4net.Config;

namespace log4net.Appender.Extended.Tests
{
    public static class Log4NetContainer
    {
        private static readonly Lazy<ILog> LazyLogger = new Lazy<ILog>(() =>
        {
            XmlConfigurator.Configure();
            return LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        });

        public static ILog Logger
        {
            get { return LazyLogger.Value; }
        }
    }
}
