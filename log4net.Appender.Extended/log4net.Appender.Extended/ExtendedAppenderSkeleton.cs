using System;
using System.Collections.Generic;
using log4net.Core;

namespace log4net.Appender.Extended
{
    public class ExtendedAppenderSkeleton : AppenderSkeleton
    {
        #region Properties

        private string _application = AppDomain.CurrentDomain.FriendlyName;
        /// <summary>
        ///     Application Name to filter logs by application, default is AppDomain.CurrentDomain.FriendlyName
        /// </summary>
        public string Application
        {
            get { return _application; }
            set { _application = value; }
        }

        private Level _environmentVariablesLevel = Level.Error;
        /// <summary>
        ///     Minimum level for Environment variables, we will include Environment Variables at this level and above. Default is Error.
        /// </summary>
        public Level EnvironmentVariablesLevel
        {
            get { return _environmentVariablesLevel; }
            set { _environmentVariablesLevel = value; }
        }

        private List<LayoutParameter> _parameters = new List<LayoutParameter>(10);

        /// <summary>
        ///     Layout parameters for custom metrics
        /// </summary>
        public List<LayoutParameter> Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        #endregion

        public void AddParameter(LayoutParameter parameter) { Parameters.Add(parameter); }

        /// <summary>
        ///     Standard extension point for AppenderSkeleton
        /// </summary>
        /// <param name="loggingEvent"></param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            var extendedLoggingEvent = ConvertLoggingEvent(loggingEvent, Parameters);
            AppendExtended(extendedLoggingEvent);
        }

        /// <summary>
        ///     Extension point for ExtendedAppenderSkeleton
        /// </summary>
        /// <param name="extendedLoggingEvent"></param>
        protected virtual void AppendExtended(ExtendedLoggingEvent extendedLoggingEvent) { }

        /// <summary>
        ///     Override this method, if you want to convert logging event to ExtendedLoggingEvent by your self.
        /// </summary>
        /// <param name="loggingEvent"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected virtual ExtendedLoggingEvent ConvertLoggingEvent(LoggingEvent loggingEvent, List<LayoutParameter> parameters)
        {
            var extendedLoggingEvent = Utility.ConvertLoggingEvent(loggingEvent, parameters, Application, EnvironmentVariablesLevel);
            return extendedLoggingEvent;
        }
    }
}