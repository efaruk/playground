using System;
using System.Collections;
using System.Runtime.Serialization;

namespace log4net.Appender.Extended
{
    /// <summary>
    ///     Serializable Exception
    /// </summary>
    [Serializable]
    [DataContract]
    public class SerializableException
    {
        /// <summary>
        ///     Default constructor
        /// </summary>
        public SerializableException() { }

        /// <summary>
        ///     Constructor with Exception parameter, it sets all values and inner exceptions recursively
        /// </summary>
        /// <param name="exception"></param>
        public SerializableException(Exception exception) { SetException(exception); }

        ///// <summary>
        ///// Type of the original exception
        ///// </summary>
        //[DataMember]
        //public Type OriginalExceptionType { get; set; }

        /// <summary>
        ///     FullName of the original exception type
        /// </summary>
        [DataMember]
        public string OriginalExceptionTypeFullName { get; set; }

        /// <summary>
        ///     Exception.Data
        /// </summary>
        [DataMember]
        public IDictionary Data { get; set; }

        /// <summary>
        ///     Exception.HelpLink
        /// </summary>
        [DataMember]
        public string HelpLink { get; set; }

        /// <summary>
        ///     Exception.HResult
        /// </summary>
        [DataMember]
        public int HResult { get; set; }

        /// <summary>
        ///     Exception.InnerException
        /// </summary>
        [DataMember]
        public SerializableException InnerException { get; set; }

        /// <summary>
        ///     Exception.Message
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        ///     Exception.Source
        /// </summary>
        [DataMember]
        public string Source { get; set; }

        /// <summary>
        ///     Exception.StackTrace
        /// </summary>
        [DataMember]
        public string StackTrace { get; set; }

        ///// <summary>
        ///// Exception.TargetSite
        ///// </summary>
        //[DataMember]
        //public MethodBase TargetSite { get; set; }

        /// <summary>
        ///     Exception.TargetSite.Name
        /// </summary>
        [DataMember]
        public string TargetSiteName { get; set; }

        /// <summary>
        ///     Sets all values and inner exceptions recursively
        /// </summary>
        /// <param name="exception"></param>
        public void SetException(Exception exception)
        {
            if (exception == null) return;
            //OriginalExceptionType = exception.GetType();
            OriginalExceptionTypeFullName = exception.GetType().FullName;
            Data = exception.Data;
            HelpLink = exception.HelpLink;
            HResult = exception.HResult;
            Message = exception.Message;
            Source = exception.Source;
            StackTrace = exception.StackTrace;
            //TargetSite = exception.TargetSite;
            TargetSiteName = (exception.TargetSite != null) ? exception.TargetSite.Name : "";
            if (exception.InnerException != null)
            {
                InnerException = new SerializableException(exception.InnerException);
            }
        }

        /// <summary>
        ///     Puke original exception (only with Message, HelpLink and Source properties, other properties are has not accessible
        ///     setters) with inner exceptions
        /// </summary>
        /// <returns>Exception</returns>
        public Exception PukeException()
        {
            var exception = InnerException != null
                ? new Exception(Message, InnerException.PukeException())
                : new Exception(Message);
            exception.HelpLink = HelpLink;
            exception.Source = Source;
            return exception;
        }
    }
}
