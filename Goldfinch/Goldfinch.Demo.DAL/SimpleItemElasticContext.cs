using System;
using ElasticLinq;

namespace Goldfinch.Demo.DAL
{
    public class SimpleItemElasticContext : ElasticSearchContext
    {

        public SimpleItemElasticContext()
            : base(GoldfinchConnectionHelper.GetNewConnection(DALSettingsWrapper.ElasticSearchEndpoint, index: DALSettingsWrapper.DefaultIndexName))
        {
            
        }

        public SimpleItemElasticContext(string uri, string index = null)
            : base(GoldfinchConnectionHelper.GetNewConnection(uri, index: index ?? DALSettingsWrapper.DefaultIndexName))
        {
            
        }
    }
}
