using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Splunk.Client;

namespace log4net.Appender.SplunkAppenders.Test
{
    [TestClass]
    public class SplunkContainerTest
    {
        public SplunkContainerTest()
        {
        }

        [TestMethod]
        public void TestCreateService()
        {
            var service = SplunkContainer.CreateSplunkService("https://localhost:8089/", "UnitTestIndex", "admin", "B0stencere").GetAwaiter().GetResult();
            Assert.IsNotNull(service.Context);
            Assert.IsNotNull(service.Context.SessionKey);
        }

        [TestMethod]
        public void TestLogging()
        {
            SplunkContainer.Log("data", "https://localhost:8089/", "UnitTestIndex", "admin", "B0stencere");
        }

        [TestMethod]
        public void TestLoggingSplunkEnterprise()
        {
            SplunkContainer.Log("data", "https://splunkforwarder:8089/", "UnitTestIndex", "ccklog4net", "Y4mukMav!");
        }

    }
}
