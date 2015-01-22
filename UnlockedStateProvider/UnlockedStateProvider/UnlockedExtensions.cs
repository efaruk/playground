using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace UnlockedStateProvider
{
	public static class UnlockedExtensions
	{
		public const string DEFAULT_APPLICATION_NAME = "UNLOCKED_STATE";
		public const string UNLOCKED_STATE_STORE_KEY = "UNLOCKED_STATE_STORE";
		public const string DEFAULT_COOKIE_NAME = "ASP.NET_SessionId";
		public const string CUSTOM_COOKIE_NAME = "UnlockedState";
		public const string SESSION_STARTED_KEY = "UNLOCKED:started";
		public const int DEFAULT_ITEM_COUNT = 15;
		public const string UNLOCKED = "UNLOCKED";


		/// <summary>
		/// <see cref="IUnlockedStateStore.SaveSession" />
		/// </summary>
		/// <param name="context"></param>
		public static void SaveSession(this HttpContext context)
		{
			var store = context.GetStoreFromContext();
			store.SaveSession();
		}

		/// <summary>
		/// <see cref="IUnlockedStateStore.SaveSession" />
		/// </summary>
		/// <param name="context"></param>
		public static void SaveSession(this ControllerContext context)
		{
			var store = context.GetStoreFromContext();
			store.SaveSession();
		}

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

		public static IUnlockedStateStore GetStoreFromCache(this HttpContext context, string cookieName)
		{
			var key = GetSessionId(context, cookieName);
			var store = (IUnlockedStateStore)context.GetCacheItem(key);
			return store;
		}

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
		public static object GetCacheItem(this HttpContext context, string key)
		{
			if (context == null) return null;
			object result = context.Cache.Get(key);
			return result;
		}

		public static void SetCacheItem(this HttpContext context, string key, object value, int sessionTimeout)
		{
			if (context == null) return;
			var expire = GetNextTimeout(sessionTimeout);
			if (context.Cache.Get(key) == null)
				context.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, expire);
			else
				context.Cache[key] = value;
		}

		public static void RemoveCacheItem(this HttpContext context, string key)
		{
			if (context == null) return;
			context.Cache.Remove(key);
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

		public static string StartSessionIfNewWithCustomCookie(string cookieName, string sessionId = "", bool useMd5 = true)
		{
			if (String.IsNullOrWhiteSpace(cookieName)) throw new ArgumentNullException("cookieName");
			var context = HttpContext.Current;
			if (context == null) return null;
			if (context.Request.Cookies.AllKeys.Contains(cookieName)) return null;
			if (context.Response.Cookies.AllKeys.Contains(cookieName)) return null;
			if (String.IsNullOrWhiteSpace(sessionId)) sessionId = Guid.NewGuid().ToString();
			var rnd = new Random(1000);
			sessionId += rnd.Next().ToString(CultureInfo.InvariantCulture);
			if (useMd5) sessionId = sessionId.ToMd5();
			if (CookieExists(context, cookieName)) return sessionId;
			var cookie = new HttpCookie(cookieName, sessionId)
			{
				Path = context.Request.ApplicationPath,
				HttpOnly = true
			};
			context.Response.Cookies.Add(cookie);
			return sessionId;
		}

		public static void EndSessionWithCustomCookie(string cookieName)
		{
			var context = HttpContext.Current;
			if (context == null) return;
			if (!CookieExists(context, cookieName)) return;
			var cookie = new HttpCookie(cookieName)
			{
				Expires = DateTime.Now.AddYears(-1)
			};
			context.Response.Cookies.Add(cookie);
		}

		public static string GetSessionId(this HttpContextBase context, string cookieName)
		{
			string result = String.Empty;
			if (context != null && context.Request.Cookies.AllKeys.Any(s => s == cookieName))
			{
				var httpCookie = context.Request.Cookies[cookieName];
				if (httpCookie != null) result = httpCookie.Value;
			}
			if (result == String.Empty)
			{
				if (context != null && context.Response != null && context.Response.Cookies != null && context.Response.Cookies.AllKeys.Any(s => s == cookieName))
				{
					var httpCookie = context.Response.Cookies[cookieName];
					if (httpCookie != null) result = httpCookie.Value;
				}
			}
			return result;
		}

		public static string GetSessionId(this ControllerContext context, string cookieName)
		{
			string result = context.HttpContext.GetSessionId(cookieName);
			return result;
		}

		public static string GetSessionId(this HttpContext context, string cookieName)
		{
			string result = String.Empty;
			if (context != null && context.Request.Cookies.AllKeys.Any(s => s == cookieName))
			{
				var httpCookie = context.Request.Cookies[cookieName];
				if (httpCookie != null) result = httpCookie.Value;
			}
			if (result == String.Empty)
			{
				if (context != null && context.Response.Cookies.AllKeys.Any(s => s == cookieName))
				{
					var httpCookie = context.Response.Cookies[cookieName];
					if (httpCookie != null) result = httpCookie.Value;
				}
			}
			return result;
		}

		public static string GetSessionId(this Controller controller, string cookieName)
		{
			string result = controller.HttpContext.GetSessionId(cookieName);
			return result;
		}

		public static string GetSessionItemKey(string keyName, string cookieName = DEFAULT_COOKIE_NAME, string sessionId = "")
		{
			if (String.IsNullOrWhiteSpace(sessionId))
				sessionId = HttpContext.Current.GetSessionId(cookieName);
			var r = String.Format("{0}:{1}:{2}", UNLOCKED, sessionId, keyName);
			return r;
		}

		public static string ToMd5(this string s)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] inputBytes = Encoding.ASCII.GetBytes(s);
			byte[] hash = md5.ComputeHash(inputBytes);
			var r = BitConverter.ToString(hash).Replace("-", String.Empty).ToLowerInvariant();
			return r;
		}

		/// <summary>
		/// Returns next session timeout as timespan from minutes.
		/// </summary>
		/// <param name="sessionTimeout">As minutes</param>
		/// <returns></returns>
		public static TimeSpan GetNextTimeout(int sessionTimeout = 20)
		{
			var timeSpan = TimeSpan.FromMinutes(sessionTimeout);
			return timeSpan;
		}

		public static bool CookieExists(this HttpContext context, string cookieName)
		{
			if (context.Request.CookieExists(cookieName) || context.Response.CookieExists(cookieName))
			{
				return true;
			}
			return false;
		}

		public static bool CookieExists(this HttpRequest request, string cookieName)
		{
			if (request.Cookies.AllKeys.Any(s => s == cookieName))
			{
				return true;
			}
			return false;
		}

		public static bool CookieExists(this HttpResponse response, string cookieName)
		{
			if (response.Cookies.AllKeys.Any(s => s == cookieName))
			{
				return true;
			}
			return false;
		}

		public static void ExpireCookie(this HttpContextBase context, string cookieName)
		{
			context.SetCookie(cookieName, "", true, "/", false, DateTime.Now.AddYears(-1));
		}

		public static void ExpireCookie(this ControllerContext context, string cookieName)
		{
			context.HttpContext.SetCookie(cookieName, "", true, "/", false, DateTime.Now.AddYears(-1));
		}

		public static void SetCookie(this HttpContextBase context, string cookieName, string value, bool httpOnly = true, string path = "/", bool isSecure = false, DateTime? expires = null)
		{
			var cookie = new HttpCookie(cookieName, value)
			{
				HttpOnly = httpOnly,
				Path = path,
				Secure = isSecure
			};
			if (expires.HasValue) cookie.Expires = expires.Value;
			context.Response.Cookies.Add(cookie);
		}

		public static void SetCookie(this ControllerContext context, string cookieName, string value, bool httpOnly = true, string path = "/", bool isSecure = false, DateTime? expires = null)
		{
			context.HttpContext.SetCookie(cookieName, value, httpOnly, path, isSecure);
		}

		public static HttpCookie GetCookie(this HttpContextBase context, string cookieName)
		{
			HttpCookie cookie = null;
			if (context.Request.Cookies.AllKeys.Any(s => s == cookieName))
			{
				cookie = context.Request.Cookies[cookieName];
			}
			return cookie;
		}

		public static HttpCookie GetCookie(this ControllerContext context, string cookieName)
		{
			return context.HttpContext.GetCookie(cookieName);
		}

		public static string ByteArrayToString(this byte[] data)
		{
			if (data == null) return null;
			string toString = Encoding.UTF8.GetString(data);
			return toString;
		}

		public static byte[] StringToByteArray(this string data)
		{
			if (string.IsNullOrWhiteSpace(data)) return null;
			var bytes = Encoding.UTF8.GetBytes(data);
			return bytes;
		}

	}
}
