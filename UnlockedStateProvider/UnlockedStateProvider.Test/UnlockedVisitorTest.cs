using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnlockedStateProvider.Test
{
	[TestClass]
	public class UnlockedVisitorTest
	{
		private readonly string _siteUrl = ConfigurationManager.AppSettings.Get("SiteUrl");
		

		[TestMethod]
		public void VisitPagesSmokely()
		{
			var cookieContainer = new CookieContainer(3);
			var response = WebHelper.MakeWebRequestAndTrackCookies(_siteUrl + "/Home", cookieContainer);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
			response = WebHelper.MakeWebRequestAndTrackCookies(_siteUrl + "/Unlocked", cookieContainer);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
			response = WebHelper.MakeWebRequestAndTrackCookies(_siteUrl + "/UnlockedReadOnly", cookieContainer);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
			response = WebHelper.MakeWebRequestAndTrackCookies(_siteUrl + "/UnlockedBig", cookieContainer);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
			response = WebHelper.MakeWebRequestAndTrackCookies(_siteUrl + "/UnlockedBigReadOnly", cookieContainer);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
		}
	}
}
