using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UnlockedStateProvider.Test
{
	public static class WebHelper
	{

		public static HttpWebResponse MakeWebRequestAndTrackCookies(string url, CookieContainer cookieContainer)
		{
			var request = WebRequest.CreateHttp(url);
			request.CookieContainer = cookieContainer;
			var response = (HttpWebResponse)request.GetResponse();
			return response;
		}

	}
}
