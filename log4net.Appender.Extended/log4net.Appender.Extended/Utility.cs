using System;
using System.Collections.Generic;
using System.Linq;
using log4net.Core;

namespace log4net.Appender.Extended
{
    public static class Utility
    {
        public const string StackTraceText = "StackTrace";
        public const string ExceptionText = "Exception";

        public static ExtendedLoggingEvent ConvertLoggingEvent(LoggingEvent loggingEvent, List<RawLayoutParameter> parameters, string application,
            Level environmentVariablesThresholdLevel)
        {
            var extendedLoggingEvent = new ExtendedLoggingEvent(loggingEvent);
            var message = loggingEvent.RenderedMessage;
            var variables = new List<KeyValuePair<string, string>>(100);
            string stackTrace = null;
            if (loggingEvent.Level >= environmentVariablesThresholdLevel)
            {
                var vars = Environment.GetEnvironmentVariables();
                variables.AddRange(from object v in vars.Keys
                    select new KeyValuePair<string, string>(v.ToString(), vars[v].ToString()));
            }
            var stackTraceParameter =
                parameters.FirstOrDefault(
                    p => string.Equals(p.ParameterName, StackTraceText, StringComparison.InvariantCultureIgnoreCase));
            var exceptionParameter =
                parameters.FirstOrDefault(
                    p => string.Equals(p.ParameterName, ExceptionText, StringComparison.InvariantCultureIgnoreCase));
            var otherParameters =
                parameters.FindAll(
                    p =>
                        !string.Equals(p.ParameterName, ExceptionText, StringComparison.InvariantCultureIgnoreCase) ||
                        !string.Equals(p.ParameterName, StackTraceText, StringComparison.InvariantCultureIgnoreCase));
            if (otherParameters.Any())
            {
                variables.AddRange(
                    otherParameters.Select(
                        parameter =>
                            new KeyValuePair<string, string>(parameter.ParameterName, parameter.Render(loggingEvent))));
            }
            if (stackTraceParameter != null)
            {
                stackTrace = stackTraceParameter.Render(loggingEvent);
            }
            if (exceptionParameter != null)
            {
                message = string.Format("{0}:\r\n {1}", message, exceptionParameter.Render(loggingEvent));
            }
            if (string.IsNullOrWhiteSpace(stackTrace))
            {
                if (loggingEvent.ExceptionObject != null && loggingEvent.ExceptionObject.StackTrace != null)
                {
                    stackTrace = loggingEvent.ExceptionObject.StackTrace;
                }
                else
                {
                    stackTrace = loggingEvent.LocationInformation.FullInfo;
                }
            }
            extendedLoggingEvent.Message = message;
            extendedLoggingEvent.StackTrace = stackTrace;
            extendedLoggingEvent.Application = application;
            extendedLoggingEvent.Variables = variables;
            foreach (var layoutParameter in parameters)
            {
                var param = new RenderedLayoutParameter(layoutParameter.ParameterName, layoutParameter.Render(loggingEvent));
                extendedLoggingEvent.EventParameters.Add(param);
                extendedLoggingEvent.Variables.Add(new KeyValuePair<string, string>(param.Name, param.Value));
            }
            return extendedLoggingEvent;
        }
    }
}