using System;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using log4net.Core;
using Splunk.Client;

namespace log4net.Appender.SplunkAppenders
{
    public static class SplunkContainer
    {
        private static System.Timers.Timer sessionTimer = new System.Timers.Timer(60000);

        static SplunkContainer() {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
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

        public static void SetSecurityProtocol(SecurityProtocolType securityProtocolType)
        {
            ServicePointManager.SecurityProtocol = securityProtocolType;
        }


        private static int sessionTimerCounter = 0;
        static void SessionTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            sessionTimerCounter++;
        }

        private static bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private static Context _splunkServiceContext;
        private static Context SplunkServiceContext
        {
            get { return _splunkServiceContext; }
            set
            {
                sessionTimerCounter = 0;
                sessionTimer.Start();
                _splunkServiceContext = value;
            }
        }

        public static async Task<Service> CreateSplunkService(string url, string indexName, string userName, string password, bool useFreshSession = false, int sessionTimeout = 55)
        {
            Service service;
            if (useFreshSession)
            {
                service = new Service(new Uri(url));
                await Logon(service, userName, password);
                await CreateIndexIfNotExist(service, indexName);
            }
            else
            {
                if (SplunkServiceContext == null || (sessionTimerCounter > sessionTimeout))
                {
                    sessionTimer.Stop();
                    service = new Service(new Uri(url));
                    SplunkServiceContext = await Logon(service, userName, password);
                    await CreateIndexIfNotExist(service, indexName);
                }
                else
                {
                    service = new Service(SplunkServiceContext);
                }
            }
            return service;
        }

        public static async Task<Context> Logon(Service service, string userName, string password)
        {
            await service.LogOnAsync(userName, password);
            return service.Context;
        }

        public static async Task<Index> CreateIndexIfNotExist(Service service, string indexName)
        {
            indexName = indexName.ToLowerInvariant();
            var indx = await service.Indexes.GetOrNullAsync(indexName) ?? await service.Indexes.CreateAsync(indexName);
            if (indx.Disabled)
                await indx.EnableAsync();
            return indx;
        }

        public static void Log(string data, string url, string indexName, string userName, string password, IErrorHandler errorHandler = null, bool useFreshSession = false, int sessionTimeout = 55)
        {
            try
            {
                indexName = indexName.ToLowerInvariant();
                var service = CreateSplunkService(url, indexName, userName, password, useFreshSession, sessionTimeout).GetAwaiter().GetResult();
                service.Transmitter.SendAsync(data, indexName).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                if (errorHandler != null)
                {
                    errorHandler.Error("Splunk Appender Log Exception.", ex);
                }
            }
        }

        public static async Task LogAsync(string data, string url, string indexName, string userName, string password, IErrorHandler errorHandler = null, bool useFreshSession = false, int sessionTimeout = 55)
        {
            try
            {
                indexName = indexName.ToLowerInvariant();
                var service = await CreateSplunkService(url, indexName, userName, password, useFreshSession, sessionTimeout);
                await service.Transmitter.SendAsync(data, indexName);
            }
            catch (Exception ex)
            {
                if (errorHandler != null)
                {
                    errorHandler.Error("Splunk Appender LogAsync Exception.", ex);
                }
            }
        }
        
    }
}
