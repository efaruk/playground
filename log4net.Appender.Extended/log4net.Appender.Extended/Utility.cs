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
        public const string AspNetRequestText = "AspNetRequest";
        public const string AspNetContextText = "AspNetContext";
        public const string AspNetSessionText = "AspNetSession";
        public const string AspNetCacheText = "AspNetCache";
        public const string AspNetServerVariablesText = "AspNetServerVariables";

        public static ExtendedLoggingEvent ConvertLoggingEvent(LoggingEvent loggingEvent, List<RawLayoutParameter> parameters, string application,
            Level environmentVariablesThresholdLevel)
        {
            var extendedLoggingEvent = new ExtendedLoggingEvent(loggingEvent);
            var message = loggingEvent.RenderedMessage;
            var variables = new List<KeyValuePair<string, string>>(100);
            string stackTrace = null;
            if (loggingEvent.Level >= environmentVariablesThresholdLevel)
            {
                variables = GetEnvironmentVariables();
            }
            var stackTraceParameter =
                parameters.FirstOrDefault(
                    p => string.Equals(p.ParameterName, StackTraceText, StringComparison.InvariantCultureIgnoreCase));
            var exceptionParameter =
                parameters.FirstOrDefault(
                    p => string.Equals(p.ParameterName, ExceptionText, StringComparison.InvariantCultureIgnoreCase));
            //var aspNetRequestParameter = parameters.FirstOrDefault(
            //        p => string.Equals(p.ParameterName, AspNetRequestText, StringComparison.InvariantCultureIgnoreCase));
            //var aspNetContextParameter = parameters.FirstOrDefault(
            //        p => string.Equals(p.ParameterName, AspNetContextText, StringComparison.InvariantCultureIgnoreCase));
            //var aspNetSessionParameter = parameters.FirstOrDefault(
            //        p => string.Equals(p.ParameterName, AspNetSessionText, StringComparison.InvariantCultureIgnoreCase));
            //var aspNetCacheParameter = parameters.FirstOrDefault(
            //        p => string.Equals(p.ParameterName, AspNetCacheText, StringComparison.InvariantCultureIgnoreCase));
            //var aspNetServerVariablesParameter = parameters.FirstOrDefault(
            //        p => string.Equals(p.ParameterName, AspNetServerVariablesText, StringComparison.InvariantCultureIgnoreCase));
            var customParameters = new[] { StackTraceText, ExceptionText, AspNetRequestText, AspNetContextText, AspNetSessionText, AspNetCacheText, AspNetServerVariablesText };
            var otherParameters =
                parameters.FindAll(
                    p => !customParameters.Contains(p.ParameterName));
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
                var exists = extendedLoggingEvent.Variables.Exists(pair =>
                {
                    if (pair.Key == param.Name) return true;
                    return false;
                });
                if (!exists)
                {
                    extendedLoggingEvent.Variables.Add(new KeyValuePair<string, string>(param.Name, param.Value));
                }
            }
            return extendedLoggingEvent;
        }

        public static List<KeyValuePair<string, string>> GetEnvironmentVariables()
        {
            var variables = new List<KeyValuePair<string, string>>(100)
            {
                new KeyValuePair<string, string>("Environment.MachineName", Environment.MachineName),
                new KeyValuePair<string, string>("Environment.CommandLine", Environment.CommandLine),
                new KeyValuePair<string, string>("Environment.CurrentDirectory", Environment.CurrentDirectory),
                new KeyValuePair<string, string>("Environment.StackTrace", Environment.StackTrace),
                new KeyValuePair<string, string>("Environment.SystemDirectory", Environment.SystemDirectory),
                new KeyValuePair<string, string>("Environment.UserDomainName", Environment.UserDomainName),
                new KeyValuePair<string, string>("Environment.UserName", Environment.UserName),
                new KeyValuePair<string, string>("Environment.OSVersion", Environment.OSVersion.VersionString),
                new KeyValuePair<string, string>("Environment.CurrentManagedThreadId", Environment.CurrentManagedThreadId.ToString()),
                new KeyValuePair<string, string>("Environment.ExitCode", Environment.ExitCode.ToString()),
                new KeyValuePair<string, string>("Environment.HasShutdownStarted", Environment.HasShutdownStarted.ToString()),
                new KeyValuePair<string, string>("Environment.Is64BitOperatingSystem", Environment.Is64BitOperatingSystem.ToString()),
                new KeyValuePair<string, string>("Environment.Is64BitProcess", Environment.Is64BitProcess.ToString()),
                new KeyValuePair<string, string>("Environment.ProcessorCount", Environment.ProcessorCount.ToString()),
                new KeyValuePair<string, string>("Environment.SystemPageSize", Environment.SystemPageSize.ToString()),
                new KeyValuePair<string, string>("Environment.TickCount", Environment.TickCount.ToString()),
                new KeyValuePair<string, string>("Environment.UserInteractive", Environment.UserInteractive.ToString()),
                new KeyValuePair<string, string>("Environment.WorkingSet", Environment.WorkingSet.ToString())
            };
            return variables;
        }

        public static List<KeyValuePair<string, string>> GetAspNetRequestVariables(LoggingEvent loggingEvent)
        {
            var data = loggingEvent.GetLoggingEventData();
            var variables = new List<KeyValuePair<string, string>>(100);
            return variables;
        }

    }
}