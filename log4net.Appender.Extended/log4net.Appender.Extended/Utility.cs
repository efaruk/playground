using System;
using System.Collections.Generic;
using System.Linq;
using log4net.Appender.Extended.Layout;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender.Extended
{
    public static class Utility
    {
        public const string StackTraceText = "StackTrace";

        public static ExtendedLoggingEvent ConvertLoggingEvent(LoggingEvent loggingEvent, List<RawLayoutParameter> parameters, string application,
            Level environmentVariablesThresholdLevel)
        {
            var extendedLoggingEvent = new ExtendedLoggingEvent(loggingEvent)
            {
                Message = loggingEvent.RenderedMessage,
                Application = application
            };
            var variables = new List<KeyValuePair<string, string>>(100);
            string stackTrace = null;
            if (loggingEvent.Level >= environmentVariablesThresholdLevel)
            {
                variables = GetEnvironmentVariables();
            }
            var otherParameters =
                parameters.FindAll(
                    p => (p.ParameterName != StackTraceText) && (p.LevelMin <= loggingEvent.Level && p.LevelMax >= loggingEvent.Level));
            if (otherParameters.Any())
            {
                foreach (var rawLayoutParameter in otherParameters)
                {
                    var param = new RenderedLayoutParameter(rawLayoutParameter.ParameterName, rawLayoutParameter.Render(loggingEvent));
                    extendedLoggingEvent.EventParameters.Add(param);
                    if (rawLayoutParameter.OmitNull)
                    {
                        if (param.Value != SystemInfo.NullText)
                        {
                            variables.Add(new KeyValuePair<string, string>(param.Name, param.Value));
                        }
                    }
                    else
                    {
                        variables.Add(new KeyValuePair<string, string>(rawLayoutParameter.ParameterName, param.Value));
                    }
                }
            }
            var stackTraceParameter =
                parameters.FirstOrDefault(
                    p => string.Equals(p.ParameterName, StackTraceText, StringComparison.InvariantCultureIgnoreCase));
            if (stackTraceParameter != null)
            {
                stackTrace = stackTraceParameter.Render(loggingEvent);
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
            }
            extendedLoggingEvent.StackTrace = stackTrace;
            extendedLoggingEvent.Variables = variables;
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

    }
}