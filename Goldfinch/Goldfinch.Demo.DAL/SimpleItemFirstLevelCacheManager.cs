using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goldfinch.Demo.Data;

namespace Goldfinch.Demo.DAL
{
    public sealed class SimpleItemFirstLevelCacheManager: FirstLevelCacheManager<SimpleItem>
    {
        public SimpleItemFirstLevelCacheManager(string defaultIndex = null): base()
        {
            if (string.IsNullOrWhiteSpace(defaultIndex)) defaultIndex = DALSettingsWrapper.DefaultIndexName;
            var uri = DALSettingsWrapper.ElasticSearchEndpoint;
            CacheStore = new ElasticSearchCacheStore<SimpleItem>(new SimpleItemElasticContext(uri, defaultIndex), uri, defaultIndex);
            PersistentRepository = new SimpleItemRepository(new GoldfinchDbContext());
        }
    }
}
