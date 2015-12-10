using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace log4net.Appender.SplunkAppenders.Test
{
    [TestClass]
    public class SplunkContainerTest
    {
        public SplunkContainerTest() { }

        [TestMethod]
        public void TestCreateService()
        {
            var service = SplunkContainer.CreateSplunkService("localhost", "UnitTestIndex", "log4net", "log4netpass");
            Assert.IsNotNull(service.Token);
        }

        [TestMethod]
        public void TestLogging()
        {
            SplunkContainer.Log("data", "localhost", "UnitTestIndex", "log4net", "log4netpass");
        }

        [TestMethod]
        public void TestLoggingSplunkEnterprise()
        {
            var data =
                "{\"Application\":\"Demo\",\"Machine\":\"BEVPC09\",\"Message\":\"Error: Home/Index:\r\n System.InvalidOperationException: Index action is not allowed for anonymous users...\r\n\",\"StackTrace\":\"log4net.Appender.SplunkAppenders.Demo.Controllers.HomeController.Index\",\"LogLevel\":\"ERROR\",\"Variables\":[{\"Key\":\"StackTrace\",\"Value\":\"log4net.Appender.SplunkAppenders.Demo.Controllers.HomeController.Index\"},{\"Key\":\"Exception\",\"Value\":\"System.InvalidOperationException: Index action is not allowed for anonymous users...\r\n\"},{\"Key\":\"StackTrace\",\"Value\":\"log4net.Appender.SplunkAppenders.Demo.Controllers.HomeController.Index\"},{\"Key\":\"Exception\",\"Value\":\"System.InvalidOperationException: Index action is not allowed for anonymous users...\r\n\"}]}";
            SplunkContainer.Log(data, "localhost", "log4net", "log4net", "log4netpass");
        }
    }
}