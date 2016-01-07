using System.Collections.Generic;
using System.Linq;

namespace log4net.Appender.Extended.Tests.TestDoubles
{
    public interface IExtendedLogStore
    {
        IQueryable<ExtendedLoggingEvent> AsQueryable { get; }
        void Add(ExtendedLoggingEvent extendedLoggingEvent);
        void Clear();
        ExtendedLoggingEvent Get(string message);
        List<ExtendedLoggingEvent> All();
        int Count();
    }
}