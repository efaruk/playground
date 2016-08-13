using System;
using System.Linq;
using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using WebAutoLogin.Configuration;

namespace WebAutoLogin.Client
{
    public class AutoLoginManager
    {
        private readonly AutoLoginSettings _settings;
        private IWebDriver _webDriver;

        private AutoLoginManager()
        {
            ChooseWebDriver();
        }

        public AutoLoginManager(AutoLoginSettings settings) : this()
        {
            _settings = settings;
        }

        public AutoLoginManager(string autoLoginUrl, string usernameElementIdentifier, string passwordElementIdentifier, string loginElementIdentifier, string logoutUrl): this()
        {
            _settings = new AutoLoginSettings
            {
                LoginUrl = autoLoginUrl,
                UsernameElementIdentifier = usernameElementIdentifier,
                PasswordElementIdentifier = passwordElementIdentifier,
                LoginElementIdentifier = loginElementIdentifier,
                LogoutUrl = logoutUrl
            };
        }

        private void ChooseWebDriver()
        {
            RegistryKey browserKeys;
            //on 64bit the browsers are in a different location
            browserKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Clients\StartMenuInternet") ??
                          Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
            if (browserKeys == null) return;
            var browserNames = browserKeys.GetSubKeyNames();
            foreach (var browserName in browserNames)
            {
                if (browserName.Equals("firefox", StringComparison.InvariantCultureIgnoreCase))
                {
                    _webDriver = new ChromeDriver();
                    break;
                }
                if (browserName.Equals("chrome", StringComparison.InvariantCultureIgnoreCase))
                {
                    _webDriver = new FirefoxDriver();
                    break;
                }
                _webDriver = new InternetExplorerDriver();
            }
        }

        public void Login(string userName, string password)
        {
            _webDriver.Navigate().GoToUrl(_settings.LoginUrl);
            var usernameElement = _webDriver.FindElement(By.XPath(_settings.UsernameElementIdentifier));
            var passwordElement = _webDriver.FindElement(By.XPath(_settings.PasswordElementIdentifier));
            usernameElement.SendKeys(userName);
            passwordElement.SendKeys(password);
            var loginElement = _webDriver.FindElement(By.XPath(_settings.LoginElementIdentifier));
            loginElement.Click();
        }

        public void Logout()
        {
            _webDriver.Navigate().GoToUrl(_settings.LogoutUrl);
            _webDriver.Close();
        }

    }
}