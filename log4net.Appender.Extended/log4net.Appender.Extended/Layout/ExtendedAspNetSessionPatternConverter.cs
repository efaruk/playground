using System.IO;
using System.Web;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender.Extended.Layout
{
    public class ExtendedAspNetSessionPatternConverter: ExtendedAspNetPatternConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent, HttpContext httpContext)
        {
            if (httpContext.Session != null)
            {
                if (Option != null)
                {
                    if (Option == WildCard)
                    {
                        foreach (var key in httpContext.Session.Keys)
                        {
                            WriteObject(writer, loggingEvent.Repository, string.Format("{0}={1};", key, httpContext.Session[key.ToString()]));
                        }
                    }
                    else {
                        WriteObject(writer, loggingEvent.Repository, httpContext.Session.Contents[Option]);
                    }
                }
                else
                {
                    WriteObject(writer, loggingEvent.Repository, httpContext.Session);
                }
            }
            else
            {
                writer.Write(SystemInfo.NotAvailableText);
            }
        }
    }
}
