using System;
using System.Collections.Generic;
using Autofac;
using log4net;
using log4net.Config;
using WebAutoLogin.Configuration;
using WebAutoLogin.Runtime.Serialization;
using WebAutoLogin.Security.Cryptography;

namespace WebAutoLogin.Client
{
    public static class DependencyContainer
    {
        private static IContainer _container;
        private static ILog _logger;

        public static void Initialize()
        {
            if (_container != null) throw new InvalidOperationException("Dependency Container already initialized.");
            var builder = new ContainerBuilder();

            // Register Logger
            _logger = LogManager.GetLogger("WebAutoLogin");
            XmlConfigurator.Configure();
            builder.RegisterInstance(_logger).As<ILog>().SingleInstance();
            builder.RegisterType<JsonNetSerializer>().As<ISerializer>().SingleInstance();
            builder.RegisterType<AppConfigSettingsProvider>().As<ISettingsProvider>().SingleInstance();
            builder.RegisterType<ApiHelper>().As<IApiHelper>().SingleInstance();
            builder.RegisterType<DefaultHashService>().As<IHashService>().SingleInstance();
            builder.RegisterType<DefaultEncryptionService>().As<IEncryptionService>().SingleInstance();

            _container = builder.Build();
        }


        // Only Use for Layout View Issue: https://github.com/autofac/Autofac/issues/349#issuecomment-33025529
        public static T Resolve<T>()
        {
            var t = _container.Resolve<T>();
            return t;
        }

        // Only Use for Layout View Issue: https://github.com/autofac/Autofac/issues/349#issuecomment-33025529
        public static IEnumerable<T> ResolveAll<T>()
        {
            var t = _container.Resolve<IEnumerable<T>>();
            return t;
        }

        // Only Use for Layout View Issue: https://github.com/autofac/Autofac/issues/349#issuecomment-33025529
        public static object Resolve(Type serviceType)
        {
            var t = _container.Resolve(serviceType);
            return t;
        }

        // Only Use for Layout View Issue: https://github.com/autofac/Autofac/issues/349#issuecomment-33025529
        public static T ResolveNamed<T>(string serviceName)
        {
            var t = _container.ResolveNamed<T>(serviceName);
            return t;
        }

        // Only Use for Layout View Issue: https://github.com/autofac/Autofac/issues/349#issuecomment-33025529
        public static object ResolveNamed(string serviceName, Type serviceType)
        {
            var t = _container.ResolveNamed(serviceName, serviceType);
            return t;
        }
    }
}
