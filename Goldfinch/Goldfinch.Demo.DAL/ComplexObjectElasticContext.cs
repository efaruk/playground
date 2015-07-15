using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ElasticLinq;
using ElasticLinq.Logging;
using ElasticLinq.Mapping;
using ElasticLinq.Retry;

namespace Goldfinch.Demo.DAL
{
    public class ComplexObjectElasticContext : ElasticSearchContext
    {

        public ComplexObjectElasticContext()
            : base(GoldfinchConnectionHelper.GetNewConnection(DALSettingsWrapper.ElasticSearchEndpoint, index: DALSettingsWrapper.DefaultIndexName))
        {
            
        }

        public ComplexObjectElasticContext(string uri, string index = null)
            : base(GoldfinchConnectionHelper.GetNewConnection(uri, index: index ?? DALSettingsWrapper.DefaultIndexName))
        {
            
        }
    }
}
