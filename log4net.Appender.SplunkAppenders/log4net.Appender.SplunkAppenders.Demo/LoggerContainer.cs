using System.Reflection;
using log4net.Config;

namespace log4net.Appender.SplunkAppenders.Demo
{
    public static class LoggerContainer
    {
        private static ILog _logger;

        static LoggerContainer()
        {
            XmlConfigurator.Configure();
            _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public static ILog Logger
        {
            get { return _logger; }
        }
    }
}