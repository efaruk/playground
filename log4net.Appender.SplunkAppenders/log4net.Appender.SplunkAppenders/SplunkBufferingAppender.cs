using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Appender.Extended;

namespace log4net.Appender.SplunkAppenders
{
    public class SplunkBufferingAppender: DoubleBufferingAppenderSkeleton
    {
        protected override void BulkSend(IEnumerable<ExtendedLoggingEvent> customLoggingEvents)
        {
            
        }
    }
}
