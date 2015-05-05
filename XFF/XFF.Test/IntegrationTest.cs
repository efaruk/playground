using System;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XFF.Test
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public void SendHeaderIntegrationTest()
        {
            var testIp = "212.212.212.212";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X_FORWARDED_FOR", testIp);
            var result = client.GetAsync("http://localhost/XFF.Demo/Default.aspx").GetAwaiter().GetResult();

            result.EnsureSuccessStatusCode();

            var content = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();


            Assert.IsTrue(content.Contains(testIp));
        }
    }
}
