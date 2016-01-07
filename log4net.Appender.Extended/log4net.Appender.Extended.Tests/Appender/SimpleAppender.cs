using log4net.Appender.Extended.Tests.TestDoubles;

namespace log4net.Appender.Extended.Tests.Appender
{
    public class SimpleAppender: ExtendedAppenderSkeleton
    {
        public SimpleAppender()
        {
            _store = TestContainerSetup.Instance.WindsorContainer.Resolve<IExtendedLogStore>(GlobalConstants.AppenderLogStore);
        }

        private readonly IExtendedLogStore _store;
        protected override void AppendExtended(ExtendedLoggingEvent extendedLoggingEvent)
        {
            _store.Add(extendedLoggingEvent);
        }
    }
}
