using System.Collections.Generic;
using log4net.Appender.Extended.Tests.TestDoubles;

namespace log4net.Appender.Extended.Tests.Appender
{
    public class SimpleDoubleBufferingAppender: DoubleBufferingAppenderSkeleton
    {
        public SimpleDoubleBufferingAppender()
        {
            _store = TestContainerSetup.Instance.WindsorContainer.Resolve<IExtendedLogStore>(GlobalConstants.BufferingAppenderLogStore);
        }

        private readonly IExtendedLogStore _store;
        protected override void BulkSend(List<ExtendedLoggingEvent> extendedLoggingEvents)
        {
            foreach (var extendedLoggingEvent in extendedLoggingEvents)
            {
                _store.Add(extendedLoggingEvent);
            }
        }
    }
}
