using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UnlockedStateProvider.Redis
{
	public class RedisUnlockedTempDataProvider : ITempDataProvider
	{
		private const string TEMP_DATA_PROVIDER_KEY = "TEMP_DATA_PROVIDER";

		public IDictionary<string, object> LoadTempData(ControllerContext controllerContext)
		{
			var result = new Dictionary<string, object>(3);
			var sessionId = controllerContext.GetSessionId(UnlockedStateStoreConfiguration.Instance.CookieName);
			if (!string.IsNullOrWhiteSpace(sessionId))
			{
				var key = UnlockedExtensions.GetSessionItemKey(TEMP_DATA_PROVIDER_KEY, "UnlockedCookie", sessionId);
				var store = new RedisUnlockedStateStore();
				var data = (Dictionary<string, object>)store.Get(key);
				if (data != null && data.Count > 0)
				{
					store.Delete(key);
					result = data;
				}
			}
			return result;
		}

		public void SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values)
		{
			if (values == null) return;
			if (values.Count == 0) return;
			var sessionId = controllerContext.GetSessionId(UnlockedStateStoreConfiguration.Instance.CookieName);
			if (!string.IsNullOrWhiteSpace(sessionId))
			{
				var key = UnlockedExtensions.GetSessionItemKey(TEMP_DATA_PROVIDER_KEY, "UnlockedCookie", sessionId);
				var store = new RedisUnlockedStateStore();
				store.Set(key, values);
			}
		}
	}
}
