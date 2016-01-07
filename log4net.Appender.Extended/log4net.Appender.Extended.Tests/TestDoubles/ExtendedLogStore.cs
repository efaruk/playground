using System.Collections.Generic;
using System.Linq;

namespace log4net.Appender.Extended.Tests.TestDoubles
{
    public class ExtendedListLogStore : IExtendedLogStore
    {
        public ExtendedListLogStore()
        {
            _list = new List<ExtendedLoggingEvent>(1000);
        }

        public ExtendedListLogStore(List<ExtendedLoggingEvent> list)
        {
            _list = list;
        }

        private readonly List<ExtendedLoggingEvent> _list;

        public IQueryable<ExtendedLoggingEvent> AsQueryable => _list.AsQueryable();


        public void Add(ExtendedLoggingEvent extendedLoggingEvent)
        {
            _list.Add(extendedLoggingEvent);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public ExtendedLoggingEvent Get(string message)
        {
            var log = _list.FirstOrDefault(e => e.Message.Contains(message));
            return log;
        }

        public List<ExtendedLoggingEvent> All()
        {
            return _list;
        }

        public int Count()
        {
            return _list.Count;
        }
    }
}
