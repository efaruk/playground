using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UnlockedStateProvider
{
	public static class ContextHelper
	{
		public static object GetContextItem(this HttpContextBase context, string key)
		{
			if (context == null) return null;
			object result = context.Items[key];
			return result;
		}

		public static object GetContextItem(this HttpContext context, string key)
		{
			if (context == null) return null;
			object result = context.Items[key];
			return result;
		}

		public static void SetContextItem(this HttpContextBase context, string key, object value)
		{
			if (context == null) return;
			context.Items[key] = value;
		}

		public static void SetContextItem(this HttpContext context, string key, object value)
		{
			if (context == null) return;
			context.Items[key] = value;
		}

		public static string GetSessionId(this HttpContextBase context, string cookieName)
		{
			string result = string.Empty;
			if (context.Request.Cookies != null && context.Request.Cookies[cookieName] != null)
			{
				result = context.Request.Cookies[cookieName].Value;
			}
			return result;
		}

		public static string GetSessionId(this HttpContext context, string cookieName)
		{
			string result = string.Empty;
			if (context.Request.Cookies != null && context.Request.Cookies[cookieName] != null)
			{
				result = context.Request.Cookies[cookieName].Value;
			}
			return result;
		}
	}
}
