using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gelastiq
{
    public class LogEvent: BaseElasticDocument
    {
        public string ApplicationId { get; set; }

        //public DateTime LogTime { get; set; }

        public string LogType { get; set; }

        public string LogMessage { get; set; }

        public string Trace { get; set; }

        public List<LogEventVariable> Variables { get; set; }
    }

    public class LogEventVariable
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
