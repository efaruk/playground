using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace UnlockedStateProvider
{
	public static class UnlockedExtensions
	{

		public static IUnlockedStateStore GetStoreFromContext(this ControllerContext context)
		{
			var store = (IUnlockedStateStore)context.GetContextItem(UnlockedStateUsageAttribute.UNLOCKED_STATE_STORE_KEY);
			return store;
		}

		public static IUnlockedStateStore GetStoreFromContext(this HttpContextBase context)
		{
			var store = (IUnlockedStateStore)context.GetContextItem(UnlockedStateUsageAttribute.UNLOCKED_STATE_STORE_KEY);
			return store;
		}

		public static IUnlockedStateStore GetStoreFromContext(this Controller controller)
		{
			var store = (IUnlockedStateStore)controller.HttpContext.GetContextItem(UnlockedStateUsageAttribute.UNLOCKED_STATE_STORE_KEY);
			return store;
		}

		public static Dictionary<string, object> GetContextItems(this ControllerContext context, string key = "")
		{
			if (string.IsNullOrWhiteSpace(key)) key = UnlockedStateUsageAttribute.UNLOCKED_STATE_OBJECT_KEY;
			var items = (Dictionary<string, object>)context.GetContextItem(UnlockedStateUsageAttribute.UNLOCKED_STATE_OBJECT_KEY);
			return items;
		}

		public static Dictionary<string, object> GetContextItems(this HttpContextBase context, string key = "")
		{
			if (string.IsNullOrWhiteSpace(key)) key = UnlockedStateUsageAttribute.UNLOCKED_STATE_OBJECT_KEY;
			var items = (Dictionary<string, object>)context.GetContextItem(UnlockedStateUsageAttribute.UNLOCKED_STATE_OBJECT_KEY);
			return items;
		}

		public static Dictionary<string, object> GetContextItems(this Controller controller, string key = "")
		{
			if (string.IsNullOrWhiteSpace(key)) key = UnlockedStateUsageAttribute.UNLOCKED_STATE_OBJECT_KEY;
			var items = (Dictionary<string, object>)controller.GetContextItem(UnlockedStateUsageAttribute.UNLOCKED_STATE_OBJECT_KEY);
			return items;
		}

		public static Dictionary<string, object> GetContextItems(this HttpContext context, string key = "")
		{
			if (string.IsNullOrWhiteSpace(key)) key = UnlockedStateUsageAttribute.UNLOCKED_STATE_OBJECT_KEY;
			var items = (Dictionary<string, object>)context.GetContextItem(UnlockedStateUsageAttribute.UNLOCKED_STATE_OBJECT_KEY);
			return items;
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

		public static string GetSessionId(this HttpContextBase context, string cookieName)
		{
			string result = string.Empty;
			if (context != null && context.Request.Cookies != null && context.Request.Cookies[cookieName] != null)
			{
				result = context.Request.Cookies[cookieName].Value;
			}
			return result;
		}

		public static string GetSessionId(this ControllerContext context, string cookieName)
		{
			string result = string.Empty;
			if (context != null && context.HttpContext.Request.Cookies != null && context.HttpContext.Request.Cookies[cookieName] != null)
			{
				result = context.HttpContext.Request.Cookies[cookieName].Value;
			}
			return result;
		}

		public static string GetSessionId(this HttpContext context, string cookieName)
		{
			string result = string.Empty;
			if (context != null && context.Request.Cookies[cookieName] != null)
			{
				result = context.Request.Cookies[cookieName].Value;
			}
			return result;
		}

		public static string GetSessionId(this Controller controller, string cookieName)
		{
			var context = controller.HttpContext;
			string result = string.Empty;
			if (context != null && context.Request.Cookies != null && context.Request.Cookies[cookieName] != null)
			{
				result = context.Request.Cookies[cookieName].Value;
			}
			return result;
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
				context.Session[UnlockedStateUsageAttribute.SESSION_STARTED_KEY] = true;
			}
		}

		public static void StartSessionIfNew(this ControllerContext context)
		{
			if (context != null && context.HttpContext != null && context.HttpContext.Session != null &&
				context.HttpContext.Session.IsNewSession && !context.HttpContext.Session.IsReadOnly)
			{
				context.HttpContext.Session[UnlockedStateUsageAttribute.SESSION_STARTED_KEY] = true;

			}
		}

		public static void StartSessionIfNew(this Controller controller)
		{
			if (controller != null && controller.HttpContext != null && controller.HttpContext.Session != null &&
				controller.HttpContext.Session.IsNewSession && !controller.HttpContext.Session.IsReadOnly)
			{
				controller.HttpContext.Session[UnlockedStateUsageAttribute.SESSION_STARTED_KEY] = true;
			}
		}

	}
}
