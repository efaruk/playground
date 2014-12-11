using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UnlockedStateProvider.Redis
{
	public class RedisUnlockedStateUsageAttribute : UnlockedStateUsageAttribute
	{
		private static readonly Lazy<IUnlockedStateStore> unlockedStateStore = new Lazy<IUnlockedStateStore>(() =>
		{
			var store = new RedisUnlockedStateStore();
			return store;
		});

		//public RedisUnlockedStateUsageAttribute()
		//{
		//	_unlockedStateStore = new RedisUnlockedStateStore();
		//}

		
		protected override IUnlockedStateStore UnlockedStateStore
		{
			get { return unlockedStateStore.Value; }
		}

		//private RedisUnlockedStateStore OverrideConfig(RedisUnlockedStateStore store)
		//{
		//	if (store == null) return null;
		//	if (!string.IsNullOrWhiteSpace(CookieName))
		//	{
		//		store.Configuration.CookieName = CookieName;
		//	}
		//	if (OperationTimeout > 0) store.Configuration.OperationTimeout = OperationTimeout;
		//	return store;
		//}

		//public override void OnActionExecuting(ActionExecutingContext filterContext)
		//{
		//	_unlockedStateStore = OverrideConfig(new RedisUnlockedStateStore());
		//	base.OnActionExecuting(filterContext);
		//}

		//public override void OnResultExecuted(ResultExecutedContext filterContext)
		//{
		//	base.OnResultExecuted(filterContext);
		//}
	}
}
