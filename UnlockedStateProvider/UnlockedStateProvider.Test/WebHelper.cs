using System;
using System.Collections.Generic;
using System.IO;
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
			HttpWebResponse response = null;
			try
			{
				response = (HttpWebResponse)request.GetResponse();
			}
			catch(Exception ex)
			{
				if (response != null)
				{
					if (response.StatusCode != HttpStatusCode.OK)
					{
						var stream = response.GetResponseStream();
						if (stream != null)
						{
							using (var sr = new StreamReader(stream))
							{
								var content = sr.ReadToEnd();
								throw new WebException(content, ex);
							}
						}
					}
				}
				else throw ex;
			}
			if (response != null) response.Close();
			return response;
		}

	}
}
