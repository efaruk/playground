using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chrono
{
    public class CustomTraceListener: TraceListener
    {

        public ITraceWriter TraceWriter { get; set; }

        public override void Write(string message)
        {
            TraceWriter.WriteTrace(message);
        }

        public override void WriteLine(string message)
        {
            TraceWriter.WriteTrace(string.Format("\r\n {0}", message));
        }
    }
}
