using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

namespace UnlockedStateProvider
{
	public class UnlockedCookieTempDataProvider : ITempDataProvider
	{
		const string UNLOCKED_TEMP_DATA_COOKIE_NAME = "Unlocked.TempData";

		public UnlockedCookieTempDataProvider(bool autoRemove = true)
		{
			AutoRemove = autoRemove;
		}

		public bool AutoRemove { get; set; }

		public IDictionary<string, object> LoadTempData(ControllerContext controllerContext)
		{
			var cookie = controllerContext.GetCookie(UNLOCKED_TEMP_DATA_COOKIE_NAME);
			if (cookie == null) return new Dictionary<string, object>(3);
			var cookieValue = cookie.Value;
			if (string.IsNullOrWhiteSpace(cookieValue)) return new Dictionary<string, object>(3);
			var tempData = new Dictionary<string, object>(3);
			try
			{
				var bytes = Convert.FromBase64String(cookieValue);
				var data = MachineKey.Unprotect(bytes);
				tempData = (Dictionary<string, object>)StateBinarySerializer.Deserialize<Dictionary<string, object>>(data);
				if (AutoRemove)
				{
					controllerContext.ExpireCookie(UNLOCKED_TEMP_DATA_COOKIE_NAME);
				}
			}
			// ReSharper disable once EmptyGeneralCatchClause
			catch { } // Omit cookie manipulations.
			return tempData;
		}

		public void SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values)
		{
			if (values == null || values.Count == 0) return;
			var bytes = StateBinarySerializer.Serialize(values);
			bytes = MachineKey.Protect(bytes);
			var cookieValue = Convert.ToBase64String(bytes);
			controllerContext.SetCookie(UNLOCKED_TEMP_DATA_COOKIE_NAME, cookieValue);
		}
	}
}
