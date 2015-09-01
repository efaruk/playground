using System.Configuration;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnlockedStateProvider.Test
{
#if DEBUG
    [TestClass]
#endif
    public class UnlockedVisitorTest
    {
        private readonly string _siteUrl = ConfigurationManager.AppSettings.Get("SiteUrl");

#if DEBUG
        [TestMethod]
#endif
        public void VisitPagesSmokely()
        {
            var cookieContainer = new CookieContainer(3);
            var response = WebHelper.MakeWebRequestAndTrackCookies((_siteUrl + "/"), cookieContainer);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            response = WebHelper.MakeWebRequestAndTrackCookies((_siteUrl + "/Unlocked"), cookieContainer);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            response = WebHelper.MakeWebRequestAndTrackCookies((_siteUrl + "/UnlockedReadOnly"), cookieContainer);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            response = WebHelper.MakeWebRequestAndTrackCookies((_siteUrl + "/UnlockedBig"), cookieContainer);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            response = WebHelper.MakeWebRequestAndTrackCookies((_siteUrl + "/UnlockedBigReadonly"), cookieContainer);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
    }
}