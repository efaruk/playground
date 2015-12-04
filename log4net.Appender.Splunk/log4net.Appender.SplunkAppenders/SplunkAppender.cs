using System;
using System.Threading.Tasks;
using log4net.Core;
using Splunk.Client;

namespace log4net.Appender.SplunkAppenders
{
    public class SplunkAppender: AppenderSkeleton
    {
        public SplunkAppender() {
            
        }

        public string SplunkUrl { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string IndexName { get; set; }

        private static Context _splunkServiceContext;
        private async Task<Service> CreateSplunkService()
        {
            Service service;
            if (_splunkServiceContext == null)
            {
                service = new Service(new Uri(SplunkUrl));
                await service.LogOnAsync(UserName, Password);
                var index = await service.Indexes.GetOrNullAsync(IndexName)
                            ?? await service.Indexes.CreateAsync(IndexName);
                await index.EnableAsync();
            }
            else
            {
                service = new Service(_splunkServiceContext);
            }
            service.
            return service;
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            var service = new Service(new Uri(SplunkUrl));
            service.Context   
        }

        protected virtual async Task<bool> Log(string jsonData)
        {
            
        }
    }
}
