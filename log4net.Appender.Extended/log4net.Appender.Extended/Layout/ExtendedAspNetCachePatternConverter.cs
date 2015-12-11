using System.IO;
using System.Web;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender.Extended.Layout
{
    public class ExtendedAspNetCachePatternConverter: ExtendedAspNetPatternConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent, HttpContext httpContext)
        {
            if (HttpRuntime.Cache != null)
            {
                if (Option != null)
                {
                    if (Option == WildCard)
                    {
                        var enumerator = HttpRuntime.Cache.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            WriteObject(writer, loggingEvent.Repository, string.Format("{0}={1};", (string)enumerator.Key, enumerator.Value));
                        }
                    }
                    else
                    {
                        WriteObject(writer, loggingEvent.Repository, HttpRuntime.Cache[Option]);
                    }
                }
                else
                {
                    WriteObject(writer, loggingEvent.Repository, HttpRuntime.Cache.GetEnumerator());
                }
            }
            else
            {
                writer.Write(SystemInfo.NotAvailableText);
            }
        }
    }
}
