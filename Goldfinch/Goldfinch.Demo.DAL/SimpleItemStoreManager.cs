using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goldfinch.Demo.Data;

namespace Goldfinch.Demo.DAL
{
    public sealed class SimpleItemStoreManager: StoreManager<SimpleItem>
    {
        public SimpleItemStoreManager(string defaultIndex = null): base()
        {
            if (string.IsNullOrWhiteSpace(defaultIndex)) defaultIndex = "goldfinch";
            var uri = ConfigurationManager.AppSettings.Get("ElasticSearchEndpoint");
            CacheStore = new ElasticSearchCacheStore<SimpleItem>(new SimpleItemElasticContext(uri, defaultIndex), uri, defaultIndex);
            PersistentRepository = new SimpleItemRepository(new GoldfinchContext());
            try
            {
                Initialize();
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch //Swallow the exception on first Initialize(), Index can be created before instance; 
            { }
        }
    }
}
