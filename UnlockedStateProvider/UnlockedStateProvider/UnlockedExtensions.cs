using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace UnlockedStateProvider
{
	public static class UnlockedExtensions
	{
		public const string DEFAULT_APPLICATION_NAME = "UNLOCKED_STATE";
		//public const string UNLOCKED_STATE_OBJECT_KEY = "UNLOCKED_STATE_OBJECT";
		public const string UNLOCKED_STATE_STORE_KEY = "UNLOCKED_STATE_STORE";
		public const string DEFAULT_COOKIE_NAME = "ASP.NET_SessionId";
		public const string SESSION_STARTED_KEY = "UNLOCKED:started";
		public const int DEFAULT_ITEM_COUNT = 15;
		public const string UNLOCKED = "UNLOCKED";

		public static IUnlockedStateStore GetStoreFromContext(this HttpContext context)
		{
			var store = (IUnlockedStateStore)context.GetContextItem(UNLOCKED_STATE_STORE_KEY);
			return store;
		}

		public static IUnlockedStateStore GetStoreFromContext(this ControllerContext context)
		{
			var store = (IUnlockedStateStore)context.GetContextItem(UNLOCKED_STATE_STORE_KEY);
			return store;
		}

		public static IUnlockedStateStore GetStoreFromContext(this HttpContextBase context)
		{
			var store = (IUnlockedStateStore)context.GetContextItem(UNLOCKED_STATE_STORE_KEY);
			return store;
		}

		public static IUnlockedStateStore GetStoreFromContext(this Controller controller)
		{
			var store = (IUnlockedStateStore)controller.HttpContext.GetContextItem(UNLOCKED_STATE_STORE_KEY);
			return store;
		}

		//public static Dictionary<string, object> GetContextItems(this ControllerContext context, string key = "")
		//{
		//	if (String.IsNullOrWhiteSpace(key)) key = UNLOCKED_STATE_OBJECT_KEY;
		//	var items = (Dictionary<string, object>)context.GetContextItem(UNLOCKED_STATE_OBJECT_KEY);
		//	return items;
		//}

		//public static Dictionary<string, object> GetContextItems(this HttpContextBase context, string key = "")
		//{
		//	if (String.IsNullOrWhiteSpace(key)) key = UNLOCKED_STATE_OBJECT_KEY;
		//	var items = (Dictionary<string, object>)context.GetContextItem(UNLOCKED_STATE_OBJECT_KEY);
		//	return items;
		//}

		//public static Dictionary<string, object> GetContextItems(this Controller controller, string key = "")
		//{
		//	if (String.IsNullOrWhiteSpace(key)) key = UNLOCKED_STATE_OBJECT_KEY;
		//	var items = (Dictionary<string, object>)controller.GetContextItem(UNLOCKED_STATE_OBJECT_KEY);
		//	return items;
		//}

		//public static Dictionary<string, object> GetContextItems(this HttpContext context, string key = "")
		//{
		//	if (String.IsNullOrWhiteSpace(key)) key = UNLOCKED_STATE_OBJECT_KEY;
		//	var items = (Dictionary<string, object>)context.GetContextItem(UNLOCKED_STATE_OBJECT_KEY);
		//	return items;
		//}

		public static object GetContextItem(this HttpContextBase context, string key)
		{
			if (context == null) return null;
			object result = context.Items[key];
			return result;
		}

		public static object GetContextItem(this ControllerContext context, string key)
		{
			if (context == null) return null;
			object result = context.HttpContext.Items[key];
			return result;
		}

		public static object GetContextItem(this HttpContext context, string key)
		{
			if (context == null) return null;
			object result = context.Items[key];
			return result;
		}

		public static object GetContextItem(this Controller controller, string key)
		{
			var context = controller.HttpContext;
			if (context == null) return null;
			object result = context.Items[key];
			return result;
		}

		public static void SetContextItem(this HttpContextBase context, string key, object value)
		{
			if (context == null) return;
			context.Items[key] = value;
		}

		public static void SetContextItem(this ControllerContext context, string key, object value)
		{
			if (context == null) return;
			context.HttpContext.Items[key] = value;
		}

		public static void SetContextItem(this HttpContext context, string key, object value)
		{
			if (context == null) return;
			context.Items[key] = value;
		}

		public static void SetContextItem(this Controller controller, string key, object value)
		{
			var context = controller.HttpContext;
			if (context == null) return;
			context.Items[key] = value;
		}

		public static bool SessionIsNew(this HttpContext context)
		{
			if (context != null && context.Session != null &&
				context.Session.IsNewSession)
			{
				return true;
			}
			return false;
		}

		public static bool SessionIsNew(this ControllerContext context)
		{
			if (context != null && context.HttpContext != null && context.HttpContext.Session != null &&
				context.HttpContext.Session.IsNewSession)
			{
				return true;
			}
			return false;
		}

		public static bool SessionIsNew(this Controller controller)
		{
			if (controller != null && controller.HttpContext != null && controller.HttpContext.Session != null &&
				controller.HttpContext.Session.IsNewSession)
			{
				return true;
			}
			return false;
		}

		public static void StartSessionIfNew(this HttpContext context)
		{
			if (context != null && context.Session != null &&
				context.Session.IsNewSession && !context.Session.IsReadOnly)
			{
				context.Session[SESSION_STARTED_KEY] = true;
			}
		}

		public static void StartSessionIfNew(this ControllerContext context)
		{
			if (context != null && context.HttpContext != null && context.HttpContext.Session != null &&
				context.HttpContext.Session.IsNewSession && !context.HttpContext.Session.IsReadOnly)
			{
				context.HttpContext.Session[SESSION_STARTED_KEY] = true;
			}
		}

		public static void StartSessionIfNew(this Controller controller)
		{
			if (controller != null && controller.HttpContext != null && controller.HttpContext.Session != null &&
				controller.HttpContext.Session.IsNewSession && !controller.HttpContext.Session.IsReadOnly)
			{
				controller.HttpContext.Session[SESSION_STARTED_KEY] = true;
			}
		}

		public static void StartSessionIfNewWithCustomCookie(string cookieName, string sessionId = "", bool useMd5 = true)
		{
			if (string.IsNullOrWhiteSpace(cookieName)) throw new ArgumentNullException("cookieName");
			var context = HttpContext.Current;
			if (context == null) return;
			if (context.Request.Cookies.AllKeys.Contains(cookieName)) return;
			if (string.IsNullOrWhiteSpace(sessionId)) sessionId = Guid.NewGuid().ToString();
			if (useMd5) sessionId = sessionId.ToMd5();
			var cookie = new HttpCookie(cookieName, sessionId)
			{
				Domain = context.Request.ServerVariables["HTTP_HOST"],
				Path = "/",
				HttpOnly = true
			};
			context.Response.Cookies.Add(cookie);
		}

		public static void EndSessionWithCustomCookie(string cookieName)
		{
			var context = HttpContext.Current;
			if (context == null) return;
			var cookie = new HttpCookie(cookieName)
			{
				Domain = context.Request.ServerVariables["HTTP_HOST"],
				Path = "/",
				HttpOnly = true,
				Expires = DateTime.Now.AddYears(-1)
			};
			context.Response.Cookies.Add(cookie);
		}

		//public static string GetSessionId(this HttpContextBase context, string cookieName)
		//{
		//	string result = String.Empty;
		//	if (context != null && context.Request.Cookies != null && context.Request.Cookies[cookieName] != null)
		//	{
		//		result = context.Request.Cookies[cookieName].Value;
		//	}
		//	if (result == string.Empty)
		//	{
		//		if (context != null && context.Response.Cookies != null && context.Response.Cookies[cookieName] != null)
		//		{
		//			result = context.Response.Cookies[cookieName].Value;
		//		}
		//	}
		//	return result;
		//}

		//public static string GetSessionId(this ControllerContext context, string cookieName)
		//{
		//	string result = context.HttpContext.GetSessionId(cookieName);
		//	return result;
		//}

		public static string GetSessionId(this HttpContext context, string cookieName)
		{
			string result = String.Empty;
			if (context != null && context.Request.Cookies[cookieName] != null)
			{
				result = context.Request.Cookies[cookieName].Value;
			}
			if (result == string.Empty)
			{
				if (context != null && context.Response != null && context.Response.Cookies != null && context.Response.Cookies[cookieName] != null)
				{
					result = context.Response.Cookies[cookieName].Value;
				}
			}
			return result;
		}

		//public static string GetSessionId(this Controller controller, string cookieName)
		//{
		//	string result = controller.HttpContext.GetSessionId(cookieName);
		//	return result;
		//}

		//public static string GetSessionKey(string cookieName = DEFAULT_COOKIE_NAME, string sessionId = "")
		//{
		//	if (String.IsNullOrWhiteSpace(sessionId))
		//		sessionId = HttpContext.Current.GetSessionId(cookieName);
		//	return String.Format("{0}:{1}", UNLOCKED, sessionId);
		//}

		//public static string GetSessionKey(this HttpContext context, string cookieName = DEFAULT_COOKIE_NAME, string sessionId = "")
		//{
		//	if (String.IsNullOrWhiteSpace(sessionId))
		//		sessionId = context.GetSessionId(cookieName);
		//	return String.Format("{0}:{1}", UNLOCKED, sessionId);
		//}

		//public static string GetSessionKey(this HttpContextBase context, string cookieName = DEFAULT_COOKIE_NAME, string sessionId = "")
		//{
		//	if (String.IsNullOrWhiteSpace(sessionId))
		//		sessionId = context.GetSessionId(cookieName);
		//	return String.Format("{0}:{1}", UNLOCKED, sessionId);
		//}

		//public static string GetSessionKey(this ControllerContext context, string cookieName = DEFAULT_COOKIE_NAME, string sessionId = "")
		//{
		//	if (String.IsNullOrWhiteSpace(sessionId))
		//		sessionId = context.GetSessionId(cookieName);
		//	return String.Format("{0}:{1}", UNLOCKED, sessionId);
		//}

		public static string GetSessionItemKey(string keyName, string cookieName = DEFAULT_COOKIE_NAME, string sessionId = "")
		{
			if (String.IsNullOrWhiteSpace(sessionId))
				sessionId = HttpContext.Current.GetSessionId(cookieName);
			var r = String.Format("{0}:{1}:{2}", UNLOCKED, sessionId, keyName);
			return r;
		}

		public static string GetSessionItemPrefix(string cookieName = DEFAULT_COOKIE_NAME, string sessionId = "")
		{
			if (String.IsNullOrWhiteSpace(sessionId))
				sessionId = HttpContext.Current.GetSessionId(cookieName);
			var r = String.Format("{0}:{1}", UNLOCKED, sessionId);
			return r;
		}

		//public static string GetSessionItemKey(this HttpContext context, string keyName, string cookieName = DEFAULT_COOKIE_NAME, string sessionId = "")
		//{
		//	if (String.IsNullOrWhiteSpace(sessionId))
		//		sessionId = context.GetSessionId(cookieName);
		//	return String.Format("{0}:{1}:{2}", UNLOCKED, sessionId, keyName);
		//}

		//public static string GetSessionItemKey(this HttpContextBase context, string keyName, string cookieName = DEFAULT_COOKIE_NAME, string sessionId = "")
		//{
		//	if (String.IsNullOrWhiteSpace(sessionId))
		//		sessionId = context.GetSessionId(cookieName);
		//	return String.Format("{0}:{1}:{2}", UNLOCKED, sessionId, keyName);
		//}

		//public static string GetSessionItemKey(this ControllerContext context, string keyName, string cookieName = DEFAULT_COOKIE_NAME, string sessionId = "")
		//{
		//	if (String.IsNullOrWhiteSpace(sessionId))
		//		sessionId = context.GetSessionId(cookieName);
		//	return String.Format("{0}:{1}:{2}", UNLOCKED, sessionId, keyName);
		//}

		public static string ToMd5(this string s)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] inputBytes = Encoding.ASCII.GetBytes(s);
			byte[] hash = md5.ComputeHash(inputBytes);
			var r = BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
			return r;
		}

		public static TimeSpan GetNextTimeout(int sessionTimeout = 20)
		{
			var timeSpan = TimeSpan.FromMinutes(sessionTimeout);
			return timeSpan;
		}
	}
}
