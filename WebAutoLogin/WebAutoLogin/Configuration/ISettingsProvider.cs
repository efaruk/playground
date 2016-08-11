using System;

namespace WebAutoLogin.Configuration
{
    public interface ISettingsProvider
    {

        string GetConnectionString(string key);

        string GetAppSetting(string key);

        string GetAppSetting(string key, string defaultValue);

        T GetNumericAppSetting<T>(string key) where T : struct;

        T GetNumericAppSetting<T>(string key, T defaultValue) where T : struct;

        int GetIntAppSetting(string key);

        int GetIntAppSetting(string key, int defaultValue);

        T GetEnumAppSetting<T>(string key, T defaultValue) where T : struct, IConvertible;

        bool GetBooleanAppSetting(string key);

        bool GetBooleanAppSetting(string key, bool defaultValue);

    }
}