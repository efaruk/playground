using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace UnlockedStateProvider
{
	public static class ContextHelper
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

		public static Dictionary<string, object> GetContextItems(this ControllerContext context)
		{
			var items = (Dictionary<string, object>)context.GetContextItem(UnlockedStateUsageAttribute.UNLOCKED_STATE_OBJECT_KEY);
			return items;
		}

		public static Dictionary<string, object> GetContextItems(this HttpContextBase context)
		{
			var items = (Dictionary<string, object>)context.GetContextItem(UnlockedStateUsageAttribute.UNLOCKED_STATE_OBJECT_KEY);
			return items;
		}

		public static Dictionary<string, object> GetContextItems(this Controller controller)
		{
			var items = (Dictionary<string, object>)controller.GetContextItem(UnlockedStateUsageAttribute.UNLOCKED_STATE_OBJECT_KEY);
			return items;
		}

		public static Dictionary<string, object> GetContextItems(this HttpContext context)
		{
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
			if (context != null && context.Request.Cookies != null && context.Request.Cookies[cookieName] != null)
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

	}
}
