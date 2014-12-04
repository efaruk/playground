using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace UnlockedStateProvider
{
	/// <summary>
	/// Define usage mode of unlocked session state.
	/// if disabled, not gets session objects from store on request begin and not push session objects to store on request end.
	/// You have to get/set objects manually using <see cref="UnlockedStateProvider" />.
	/// </summary>
	[AttributeUsageAttribute(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	public abstract class UnlockedStateUsageAttribute: ActionFilterAttribute
	{
		public const string UNLOCKED_STATE_OBJECT_KEY = "UNLOCKED_STATE_OBJECT_KEY";
		public const string UNLOCKED_STATE_STORE_KEY = "UNLOCKED_STATE_STORE_KEY";
		public const string DEFAULT_COOKIE_NAME = "ASP.NET_SessionId";
		public const string SESSION_STARTED_KEY = "UNLOCKED:started";
		public const int DEFAULT_ITEM_COUNT = 15;

		/// <summary>
		/// if disabled, not gets session objects from store on request begin and not push session objects to store on request end.
		/// You have to get/set objects manually using <see cref="UnlockedStateProvider" />.
		/// </summary>
		public UnlockedStateUsage Usage { get; set; }

		private string _cookieName = DEFAULT_COOKIE_NAME;
		public string CookieName
		{
			get { return _cookieName; }
			set { _cookieName = value; }
		}

		public int Timeout { get; set; }

		/// <summary>
		/// If true, runs set, delete, evaluates requests in async mode.
		/// </summary>
		public bool RunAsync { get; set; }

		abstract protected IUnlockedStateStore UnlockedStateStore { get; }

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			// filterContext.StartSessionIfNew();
			var store = UnlockedStateStore;
			var session = store.Get(GetSessionKey()) ?? new Dictionary<string, object>(DEFAULT_ITEM_COUNT);
			filterContext.SetContextItem(UNLOCKED_STATE_OBJECT_KEY, session);
			filterContext.SetContextItem(UNLOCKED_STATE_STORE_KEY, store);
			base.OnActionExecuting(filterContext);
		}

		//public override void OnActionExecuted(ActionExecutedContext filterContext)
		//{
		//	base.OnActionExecuted(filterContext);
		//}

		//public override void OnResultExecuting(ResultExecutingContext filterContext)
		//{
		//	base.OnResultExecuting(filterContext);
		//}

		public override void OnResultExecuted(ResultExecutedContext filterContext)
		{
			var session = filterContext.HttpContext.GetContextItem(UNLOCKED_STATE_OBJECT_KEY);
			if (session != null && ((ICollection)session).Count > 0)
			{
				filterContext.StartSessionIfNew();
				var store = (IUnlockedStateStore)filterContext.GetContextItem(UNLOCKED_STATE_STORE_KEY);
				store.UpdateContext();
				var expire = DateTime.Now.AddMinutes(Timeout).TimeOfDay;
				store.Set(GetSessionKey(), session, expire, RunAsync);
			}
			base.OnResultExecuted(filterContext);
		}

		private string GetSessionKey(string sessionId = "")
		{
			if (string.IsNullOrWhiteSpace(sessionId))
				sessionId = HttpContext.Current.GetSessionId(CookieName);
			return string.Format("{0}:{1}", "UNLOCKED", sessionId);
		}

	}
}
