using System.IO;
using System.Web;
using log4net.Core;
using log4net.Layout.Pattern;
using log4net.Util;

namespace log4net.Appender.Extended.Layout
{
    public abstract class ExtendedAspNetPatternConverter : PatternLayoutConverter
    {
        public const string WildCard = "*";
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            if (HttpContext.Current == null)
            {
                writer.Write(SystemInfo.NotAvailableText);
            }
            else
            {
                Convert(writer, loggingEvent, HttpContext.Current);
            }
        }

        /// <summary>
        /// Derived pattern converters must override this method in order to
        /// convert conversion specifiers in the correct way.
        /// </summary>
        /// <param name="writer"><see cref="TextWriter" /> that will receive the formatted result.</param>
        /// <param name="loggingEvent">The <see cref="LoggingEvent" /> on which the pattern converter should be executed.</param>
        /// <param name="httpContext">The <see cref="HttpContext" /> under which the ASP.Net request is running.</param>
        protected abstract void Convert(TextWriter writer, LoggingEvent loggingEvent, HttpContext httpContext);
    }
}
