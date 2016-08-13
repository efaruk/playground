namespace WebAutoLogin.Configuration
{
    public class AutoLoginSettings
    {
        public AutoLoginSettings()
        {
            
        }

        public AutoLoginSettings(ISettingsProvider settingsProvider)
        {
            LoginUrl = settingsProvider.GetAppSetting("LoginUrl");
            UsernameElementIdentifier = settingsProvider.GetAppSetting("UsernameElementIdentifier");
            PasswordElementIdentifier = settingsProvider.GetAppSetting("PasswordElementIdentifier");
            LoginElementIdentifier = settingsProvider.GetAppSetting("LoginElementIdentifier");
            LogoutUrl = settingsProvider.GetAppSetting("LogoutUrl");
        }
        
        public string LoginUrl { get; set; }

        public string UsernameElementIdentifier { get; set; }

        public string PasswordElementIdentifier { get; set; }

        public string LoginElementIdentifier { get; set; }

        public string LogoutUrl { get; set; }
    }
}