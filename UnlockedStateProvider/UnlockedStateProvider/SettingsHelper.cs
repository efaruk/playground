using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnlockedStateProvider
{
	public static class SettingsHelper
	{
		public static string GetAppSetting(string key, string defaultValue = "")
		{
			var value = ConfigurationManager.AppSettings.Get(key);
			if (string.IsNullOrEmpty(value))
			{
				value = defaultValue;
			}
			return value;
		}

		public static int GetIntAppSetting(string key, int defaultValue)
		{
			var value = ConfigurationManager.AppSettings.Get(key);
			if (string.IsNullOrEmpty(value))
			{
				value = defaultValue.ToString(CultureInfo.InvariantCulture);
			}
			return int.Parse(value);
		}

		public static bool GetBoolAppSetting(string key, bool defaultValue)
		{
			var value = ConfigurationManager.AppSettings.Get(key);
			if (string.IsNullOrEmpty(value))
			{
				value = defaultValue.ToString(CultureInfo.InvariantCulture);
			}
			return bool.Parse(value);
		}

	}
}
