using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFF
{
    public static class SettingsWrapper
    {
        public const string DEFAULT_SEPARATOR = ",";
        public const string DEFAULT_HEADER_HTTP_X_FORWARDED_FOR = "HTTP_X_FORWARDED_FOR";
        public const int DEFAULT_INDEX = 0;
        public const string HEADER_REMOTE_ADDR = "REMOTE_ADDR";

        private static readonly string ForwardedHeaderSetting = ConfigurationManager.AppSettings.Get("X-Forwarded-Header");
        private static readonly string SeparatorSetting = ConfigurationManager.AppSettings.Get("X-Forwarded-Ip-Separator");
        private static readonly string ClientIpIndexSetting = ConfigurationManager.AppSettings.Get("X-Forwarded-ClientIp-Index");

        static SettingsWrapper()
        {
            ForwardedHeader = string.IsNullOrWhiteSpace(ForwardedHeaderSetting) ? DEFAULT_HEADER_HTTP_X_FORWARDED_FOR : ForwardedHeaderSetting;
            Separator = string.IsNullOrWhiteSpace(SeparatorSetting) ? DEFAULT_SEPARATOR : SeparatorSetting;
            ClientIpIndex = string.IsNullOrWhiteSpace(ClientIpIndexSetting) ? DEFAULT_INDEX : int.Parse(ClientIpIndexSetting);
        }


        public static string ForwardedHeader { get; private set; }

        public static string Separator { get; private set; }

        public static int ClientIpIndex { get; private set; }
    }
}
