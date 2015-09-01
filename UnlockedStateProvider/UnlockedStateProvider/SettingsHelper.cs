using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;

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


        //Taken from MS
        public static string GetStringSettings(NameValueCollection config, string attrName, string defaultVal)
        {
            string fromConfig = GetFromConfig(config, attrName);
            if (string.IsNullOrEmpty(fromConfig))
                return defaultVal;
            string fromAppSetting = GetFromAppSetting(fromConfig);
            if (!string.IsNullOrEmpty(fromAppSetting))
                return fromAppSetting;
            else
                return fromConfig;
        }

        public static int GetIntSettings(NameValueCollection config, string attrName, int defaultVal)
        {
            string str = (string) null;
            try
            {
                str = GetFromConfig(config, attrName);
                if (str == null)
                    return defaultVal;
                else
                    return int.Parse(str);
            }
            catch (FormatException ex)
            {
            }
            string fromAppSetting = GetFromAppSetting(str);
            if (fromAppSetting == null)
                return int.Parse(str);
            else
                return int.Parse(fromAppSetting);
        }

        public static bool GetBoolSettings(NameValueCollection config, string attrName, bool defaultVal)
        {
            var attrName1 = (string) null;
            try
            {
                attrName1 = GetFromConfig(config, attrName);
                if (attrName1 == null)
                    return defaultVal;
                else
                    return bool.Parse(attrName1);
            }
            catch (FormatException ex)
            {
            }
            string fromAppSetting = GetFromAppSetting(attrName1);
            if (fromAppSetting == null)
                return bool.Parse(attrName1);
            else
                return bool.Parse(fromAppSetting);
        }

        public static string GetFromAppSetting(string attrName)
        {
            if (!string.IsNullOrEmpty(attrName))
            {
                string str = ConfigurationManager.AppSettings[attrName];
                if (!string.IsNullOrEmpty(str))
                    return str;
            }
            return (string) null;
        }

        public static string GetFromConfig(NameValueCollection config, string attrName)
        {
            string[] values = config.GetValues(attrName);
            if (values != null && values.Length > 0 && !string.IsNullOrEmpty(values[0]))
                return values[0];
            else
                return (string) null;
        }
    }
}