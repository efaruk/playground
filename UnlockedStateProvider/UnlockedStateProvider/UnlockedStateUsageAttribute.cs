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
		//protected UnlockedStateUsageAttribute()
		//{
		//	// ReSharper disable DoNotCallOverridableMethodsInConstructor
		//	//if (UnlockedStateStore != null)
		//	//{
		//	//	_store = UnlockedStateStore;
		//	//	if (!string.IsNullOrWhiteSpace(CookieName))
		//	//	{
		//	//		_store.Configuration.CookieName = CookieName;
		//	//	}
		//	//}
		//	// ReSharper restore DoNotCallOverridableMethodsInConstructor
		//}


		private UnlockedStateUsage _usage = UnlockedStateUsage.ReadWrite;
		/// <summary>
		/// if disabled, not gets session objects from store on request begin and not push session objects to store on request end.
		/// You have to get/set objects manually using <see cref="UnlockedStateProvider" />.
		/// </summary>
		public UnlockedStateUsage Usage
		{
			get { return _usage; }
			set { _usage = value; }
		}

		private string _cookieName;
		/// <summary>
		/// Override configuration cookie or define by controller
		/// </summary>
		public string CookieName
		{
			get
			{
				return _cookieName;
			}
			set
			{
				_cookieName = value;
				//if (string.IsNullOrWhiteSpace(_cookieName) && UnlockedStateStore != null)
				//{
				//	UnlockedStateStore.Configuration.CookieName = _cookieName;
				//}
			}
		}

		private int _operationOperationTimeout;
		/// <summary>
		/// Operation timeout as seconds.
		/// </summary>
		public int OperationTimeout
		{
			get { return _operationOperationTimeout; }
			set
			{
				_operationOperationTimeout = value;
				//if (_operationOperationTimeout != 0 && UnlockedStateStore != null)
				//{
				//	UnlockedStateStore.Configuration.OperationTimeout = _operationOperationTimeout;
				//}
			}
		}

		/// <summary>
		/// If true, runs set, delete, evaluate requests in async mode.
		/// </summary>
		public bool RunAsync { get; set; }

		//private readonly IUnlockedStateStore _store;


		abstract protected IUnlockedStateStore UnlockedStateStore { get; }

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (Usage != UnlockedStateUsage.Disabled)
			{
				// filterContext.StartSessionIfNew();
				//var store = UnlockedStateStore;
				var session = (Dictionary<string, object>)UnlockedStateStore.Get(UnlockedExtensions.UNLOCKED_STATE_STORE_KEY) ?? new Dictionary<string, object>(UnlockedExtensions.DEFAULT_ITEM_COUNT);
				UnlockedStateStore.Items = session;
				//filterContext.SetContextItem(UnlockedExtensions.UNLOCKED_STATE_OBJECT_KEY, session);
				filterContext.SetContextItem(UnlockedExtensions.UNLOCKED_STATE_STORE_KEY, UnlockedStateStore);
			}
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
			if (Usage == UnlockedStateUsage.ReadWrite)
			{
				//var session = filterContext.HttpContext.GetContextItem(UNLOCKED_STATE_OBJECT_KEY);
				if (UnlockedStateStore.Items.Count > 0)
				{
					//filterContext.StartSessionIfNew();
					//var store = (IUnlockedStateStore)filterContext.GetContextItem(UNLOCKED_STATE_STORE_KEY);
					// store.UpdateContext();
					var expire = UnlockedExtensions.GetNextTimeout(UnlockedStateStore.Configuration.SessionTimeout);
					UnlockedStateStore.Set(UnlockedExtensions.UNLOCKED_STATE_STORE_KEY, UnlockedStateStore.Items, expire, RunAsync);
				}
			}
			base.OnResultExecuted(filterContext);
			//UnlockedStateStore.Dispose();
		}

	}
}
