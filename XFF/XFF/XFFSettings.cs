using System.Configuration;
using System.Linq;

namespace XFF
{
    public class XFFSettings : ConfigurationSection
    {
        private static readonly XFFSettings _settings = ConfigurationManager.GetSection("XFFSettings") as XFFSettings ?? new XFFSettings();

        public static XFFSettings Settings
        {
            get
            {
                return _settings;
            }
        }

        [ConfigurationProperty("headerName", DefaultValue = "HTTP_X_FORWARDED_FOR", IsRequired = false)]
        public string ApplicationId
        {
            get
            {
                return (string)this["headerName"];
            }
            set
            {
                this["headerName"] = value;
            }
        }

        [ConfigurationProperty("separator", DefaultValue = ",", IsRequired = false)]
        public string Environment
        {
            get
            {
                return (string)this["separator"];
            }
            set
            {
                this["separator"] = value;
            }
        }


        [ConfigurationProperty("clientIpIndex", DefaultValue = 0, IsRequired = false)]
        public int ApiEndpoint
        {
            get
            {
                return (int)this["clientIpIndex"];
            }
            set
            {
                this["clientIpIndex"] = value;
            }
        }

        [ConfigurationProperty("disabled", DefaultValue = false, IsRequired = false)]
        public bool Disabled
        {
            get
            {
                return (bool)this["disabled"];
            }
            set
            {
                this["disabled"] = value;
            }
        }

        public override bool IsReadOnly()
        {
            return false;
        }

    }
}
