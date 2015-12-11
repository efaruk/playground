using System;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using log4net.Core;
using Splunk;

namespace log4net.Appender.SplunkAppenders
{
    public static class SplunkContainer
    {
        private static System.Timers.Timer sessionTimer = new System.Timers.Timer(60000);

        static SplunkContainer()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            DisableCertificateValidation();
            sessionTimer.Elapsed += SessionTimer_Elapsed;
        }

        public static void DisableCertificateValidation()
        {
            if (ServicePointManager.ServerCertificateValidationCallback == null)
            {
                ServicePointManager.ServerCertificateValidationCallback += ServerCertificateValidationCallback;
            }
        }

        public static void EnableCertificateValidation()
        {
            if (ServicePointManager.ServerCertificateValidationCallback != null)
            {
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback -= ServerCertificateValidationCallback;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }

        public static void SetSecurityProtocol(SecurityProtocolType securityProtocolType) { ServicePointManager.SecurityProtocol = securityProtocolType; }


        private static int sessionTimerCounter = 0;
        static void SessionTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) { sessionTimerCounter++; }

        private static bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; }

        private static string _splunkToken;

        private static string SplunkToken
        {
            get { return _splunkToken; }
            set
            {
                sessionTimerCounter = 0;
                sessionTimer.Start();
                _splunkToken = value;
            }
        }

        public static Service CreateSplunkService(string host, string indexName, string userName, string password, int port = 8089, bool useFreshSession = false, int sessionTimeout = 55)
        {
            Service service;
            if (useFreshSession)
            {
                service = new Service(host, port);
                Logon(service, userName, password);
                CreateIndexIfNotExist(service, indexName);
            }
            else
            {
                if (string.IsNullOrEmpty(SplunkToken) || (sessionTimerCounter > sessionTimeout))
                {
                    sessionTimer.Stop();
                    service = new Service(host, port);
                    SplunkToken = Logon(service, userName, password);
                    CreateIndexIfNotExist(service, indexName);
                }
                else
                {
                    service = new Service(host, port);
                    service.Token = SplunkToken;
                }
            }
            return service;
        }

        public static string Logon(Service service, string userName, string password)
        {
            service.Login(userName, password);
            return service.Token;
        }

        public static void CreateIndexIfNotExist(Service service, string indexName)
        {
            indexName = indexName.ToLowerInvariant();
            var indexes = service.GetIndexes();
            if (!indexes.ContainsKey(indexName))
            {
                indexes.Create(indexName);
                indexes.Refresh();   
            }
            var indx = indexes.Get(indexName);
            if (indx.IsDisabled)
            {
                indx.Enable();
            }
        }



        public static void Log(string data, string host, string indexName, string userName, string password, int port = 8089, IErrorHandler errorHandler = null, bool useFreshSession = false,
            int sessionTimeout = 55)
        {
            try
            {
                indexName = indexName.ToLowerInvariant();
                var service = CreateSplunkService(host, indexName, userName, password, port, useFreshSession, sessionTimeout);
                var receiver = service.GetReceiver();
                var args = new ReceiverSubmitArgs()
                {
                    Index = indexName,
                    Source = "SplunkAppender",
                    SourceType = "bigjson",
                    Host = Environment.MachineName
                };
                receiver.Submit(args, data);
            }
            catch (Exception ex)
            {
                if (errorHandler != null)
                {
                    errorHandler.Error("Splunk Appender Log Exception.", ex);
                }
            }
        }

        public static async Task LogAsync(string data, string host, string indexName, string userName, string password, int port = 8089, IErrorHandler errorHandler = null, bool useFreshSession = false,
            int sessionTimeout = 55)
        {
            var t = new Task(() =>
            {
                try
                {
                    Log(data, host, indexName, userName, password, port, errorHandler, useFreshSession, sessionTimeout);
                }
                catch (Exception ex)
                {
                    if (errorHandler != null)
                    {
                        errorHandler.Error("Splunk Appender Log Exception.", ex);
                    }
                }
            });
            t.Start();
            await t;
        }
    }
}