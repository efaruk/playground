using System.Linq;
using System.Net.Http;
using Autofac;
using log4net;
using log4net.Config;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using WebAutoLogin.Client;
using WebAutoLogin.Configuration;
using WebAutoLogin.Security.Cryptography;
using WebAutoLogin.Service.Data;
using WebAutoLogin.Service.Services;

namespace WebAutoLogin.Service
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private ILog _logger;

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            // Register Logger
            _logger = LogManager.GetLogger("WebAutoLogin");
            XmlConfigurator.Configure();

            container.Update(builder => builder.RegisterInstance(_logger).As<ILog>().SingleInstance());
            container.Update(builder => builder.RegisterType<AppConfigSettingsProvider>().As<ISettingsProvider>().SingleInstance());
            container.Update(builder => builder.RegisterType<DefaultHashService>().As<IHashService>().SingleInstance());
            container.Update(builder => builder.RegisterType<DefaultEncryptionService>().As<IEncryptionService>().SingleInstance());
            container.Update(builder => builder.RegisterType<DataService>().As<IDataService>().InstancePerLifetimeScope());
            container.Update(builder => builder.RegisterType<ApiService>().As<IApiService>().InstancePerLifetimeScope());
        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            pipelines.BeforeRequest += ctx =>
            {
                if (!ctx.Request.Path.StartsWith("/accounts/")) return null;
                if (ctx.Request.Path.StartsWith("/accounts/token/"))
                {
                    return null;
                }
                var response = new Response { StatusCode = HttpStatusCode.Forbidden };
                if (!ctx.Request.Headers.Keys.Contains(GlobalModule.TokenHeaderKey))
                {
                    return response;
                }
                var service = container.Resolve<IApiService>();
                var token = ctx.Request.Headers[GlobalModule.TokenHeaderKey].First().ToString();
                var account = service.GetAccountByToken(token);
                return account == null ? response : null;
            };

            base.RequestStartup(container, pipelines, context);
        }
    }
}