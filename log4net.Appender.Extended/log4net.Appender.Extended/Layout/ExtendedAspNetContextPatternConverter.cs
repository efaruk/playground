using System.IO;
using System.Web;
using log4net.Core;

namespace log4net.Appender.Extended.Layout
{
    public class ExtendedAspNetContextPatternConverter: ExtendedAspNetPatternConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent, HttpContext httpContext)
        {
            if (Option != null)
            {
                if (Option == WildCard)
                {
                    foreach (var key in httpContext.Items.Keys)
                    {
                        WriteObject(writer, loggingEvent.Repository, string.Format("{0}={1};", key, httpContext.Items[key]));
                    }
                }
                else
                {
                    WriteObject(writer, loggingEvent.Repository, httpContext.Items[Option]);
                }
            }
            else
            {
                WriteObject(writer, loggingEvent.Repository, httpContext.Items);
            }
        }
    }
}