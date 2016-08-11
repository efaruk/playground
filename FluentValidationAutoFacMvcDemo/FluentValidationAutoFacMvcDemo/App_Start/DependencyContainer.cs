using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using FluentValidation;
using FluentValidation.Mvc;
using FluentValidAutoFacMvcDemo.Infrastructure;
using log4net;
using log4net.Config;

namespace FluentValidAutoFacMvcDemo
{
    public static class DependencyContainer
    {
        private static IContainer _container;
        private static ILog _logger;
        private static AppDomainTypeFinder _typeFinder;

        public static void Initialize()
        {
            if (_container != null) throw new InvalidOperationException("Dependency Container already initialized.");
            var builder = new ContainerBuilder();
            _typeFinder = new AppDomainTypeFinder();
            // Register Logger
            _logger = LogManager.GetLogger("Demo");
            XmlConfigurator.Configure();
            builder.RegisterInstance(_logger).As<ILog>().SingleInstance();
            builder.RegisterInstance(_typeFinder).As<ITypeFinder>().SingleInstance();

            // Register Web Types
            builder.RegisterModule(new AutofacWebTypesModule());

            //// Closed for Layout View Issue: https://github.com/autofac/Autofac/issues/349#issuecomment-33025529
            // Register ViewPages
            //builder.RegisterSource(new ViewRegistrationSource());

            // Register Controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register Action Filters
            builder.RegisterFilterProvider();

            //FluentValidation
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ModelValidatorProviders.Providers.Clear();
            FluentValidationModelValidatorProvider.Configure(provider =>
            {
                provider.ValidatorFactory = new AutofacValidatorFactory();
                provider.AddImplicitRequiredValidator = false;
            });
            //var fluentValidation = new FluentValidationModelValidatorProvider(new AutofacValidatorFactory())
            //{
            //    AddImplicitRequiredValidator = false
            //};
            //// builder.RegisterType<FluentValidationModelValidatorProvider>().As<ModelValidatorProvider>();
            //builder.RegisterInstance(fluentValidation).As<ModelValidatorProvider>();
            //builder.RegisterType<AutofacValidatorFactory>().As<IValidatorFactory>().SingleInstance();
            // Register IValidator's
            FindAndRegisterValidators(builder);


            // Build: NO MORE REGISTRATION ALLOWED ABOVE
            _container = builder.Build();

            // Set MVC DependencyResolver to integrate
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
        }

        private static void FindAndRegisterValidators(ContainerBuilder builder)
        {
            var validators = _typeFinder.FindClassesOfType<IValidator>();
            foreach (var type in validators)
            {
                builder.RegisterType(type).AsImplementedInterfaces().InstancePerLifetimeScope();
            }
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