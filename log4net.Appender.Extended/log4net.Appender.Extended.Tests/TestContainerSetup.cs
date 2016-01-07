using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using log4net.Appender.Extended.Tests.TestDoubles;

namespace log4net.Appender.Extended.Tests
{
    public class TestContainerSetup
    {
        private static readonly TestContainerSetup SingleInstance = new TestContainerSetup();

        public static TestContainerSetup Instance { get { return SingleInstance; } }


        private readonly IWindsorContainer _container;
        
        private TestContainerSetup()
        {
            _container = new WindsorContainer();
            var installer = new TestContainerSetupInstaller();
            _container.Install(installer);
        }

        private TestContainerSetup(IWindsorContainer container)
        {
            _container = container;
            var installer = new TestContainerSetupInstaller();
            _container.Install(installer);
        }

        public IWindsorContainer WindsorContainer
        {
            get { return _container; }
        }
    }

    public class TestContainerSetupInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IExtendedLogStore>().ImplementedBy<ExtendedListLogStore>().Named(GlobalConstants.BufferingAppenderLogStore).LifestyleSingleton());
            container.Register(Component.For<IExtendedLogStore>().ImplementedBy<ExtendedListLogStore>().Named(GlobalConstants.AppenderLogStore).LifestyleSingleton());
        }
    }
}
