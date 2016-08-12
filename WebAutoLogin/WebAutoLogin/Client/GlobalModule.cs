using WebAutoLogin.Data.Entities;

namespace WebAutoLogin.Client
{
    public static class GlobalModule
    {
        public static Account Account { get; set; }

        public static string StickPattern = "sabet";

        public static int NotificationTimeout = 1000;

        public static string TokenHashFormat = "{0}|{1}"; // {0} = username, {1} = password

        public static string TokenHeaderKey = "token";

        public static string SettingBaseAddress = "BaseAddress";
        // AutoLoginUrl, UsernameInputIdentifier, PasswordInputIdentifier, SubmitButtonIdentifier

        public static string SettingKey = "Key";

        public static string SettingVector = "Vector";

        public static string SettingApiUrl = "ApiUrl";
    }
}