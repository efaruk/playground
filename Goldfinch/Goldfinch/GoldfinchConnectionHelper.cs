using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticLinq;

namespace Goldfinch
{
    public static class GoldfinchConnectionHelper
    {
        public static ElasticConnection GetNewConnection(string url, string index = null, TimeSpan? timeout = null, ElasticConnectionOptions options = null)
        {
            var uri = new Uri(url);
            var username = "";
            var password = "";
            if (!string.IsNullOrWhiteSpace(uri.UserInfo))
            {
                var upa = uri.UserInfo.Split(new[] {":"}, StringSplitOptions.RemoveEmptyEntries);
                username = upa[0];
                password = upa[1];
            }
            var connection = new ElasticConnection(uri, username, password, timeout, index, options);
            return connection;
        }
    }
}
