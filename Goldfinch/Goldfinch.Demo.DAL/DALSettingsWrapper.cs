using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Goldfinch.Demo.DAL
{
    public static class DALSettingsWrapper
    {
        private static string _defaultIndexName = "goldfinch";
        public static string DefaultIndexName
        {
            get { return _defaultIndexName; }
            set { _defaultIndexName = value; }
        }


        private static string elasticSearchEndpoint = ConfigurationManager.AppSettings.Get("ElasticSearchEndpoint");

        public static string ElasticSearchEndpoint
        {
            get { return elasticSearchEndpoint; }
            set { elasticSearchEndpoint = value; }
        }
    }
}
