namespace WebAutoLogin.Configuration
{
    public class ServiceSettings
    {
        public ServiceSettings()
        {
            
        }

        public ServiceSettings(ISettingsProvider settingsProvider)
        {
            AutoLoginUrl = settingsProvider.GetAppSetting("AutoLoginUrl");
            UsernameInputIdentifier = settingsProvider.GetAppSetting("UsernameInputIdentifier");
            PasswordInputIdentifier = settingsProvider.GetAppSetting("PasswordInputIdentifier");
            SubmitButtonIdentifier = settingsProvider.GetAppSetting("SubmitButtonIdentifier");
        }

        public string AutoLoginUrl { get; set; }

        public string UsernameInputIdentifier { get; set; }

        public string PasswordInputIdentifier { get; set; }

        public string SubmitButtonIdentifier { get; set; }
    }
}