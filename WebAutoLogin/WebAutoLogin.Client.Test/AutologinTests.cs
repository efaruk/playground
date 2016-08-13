using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAutoLogin.Configuration;

namespace WebAutoLogin.Client.Test
{
    [TestClass]
    public class AutologinTests
    {
        [TestMethod]
        public void TestBestCaseSenario()
        {
            var settings = new AutoLoginSettings(new AppConfigSettingsProvider());
            var manager = new AutoLoginManager(settings);

            manager.Login("abuzerkadayif", "C0mplexP4$$w0rd!");

            Thread.Sleep(5000);

            manager.Logout();

            Thread.Sleep(1000);

            manager.Login("abuzerkadayif", "C0mplexP4$$w0rd!");

            Thread.Sleep(5000);

            manager.Logout();
        }
    }
}
