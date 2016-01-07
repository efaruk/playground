using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace log4net.Appender.Extended.Tests
{
    [TestClass]
    public class SetupAssemblyInitializer
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            var testSetup = TestContainerSetup.Instance;
        }
    }
}
