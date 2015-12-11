using System.IO;
using System.Web;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender.Extended.Layout
{
    public class ExtendedAspNetRequestPatternConverter: ExtendedAspNetPatternConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent, HttpContext httpContext)
        {
            HttpRequest request = null;
            try
            {
                request = httpContext.Request;
            }
            catch (HttpException)
            {
                // likely a case of running in IIS integrated mode
                // when inside an Application_Start event.
                // treat it like a case of the Request
                // property returning null
            }

            if (request != null)
            {
                if (Option != null)
                {
                    if (Option == WildCard)
                    {
                        foreach (var key in httpContext.Request.Params.AllKeys)
                        {
                            WriteObject(writer, loggingEvent.Repository, string.Format("{0}={1};", key, httpContext.Request.Params[key]));
                        }
                    }
                    else
                    {
                        WriteObject(writer, loggingEvent.Repository, httpContext.Request.Params[Option]);
                    }
                }
                else
                {
                    WriteObject(writer, loggingEvent.Repository, httpContext.Request.Params);
                }
            }
            else
            {
                writer.Write(SystemInfo.NotAvailableText);
            }
        }
    }
}
