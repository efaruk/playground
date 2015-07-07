using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goldfinch.Demo.Data;

namespace Goldfinch.Demo.DAL
{
    public sealed class ComplexObjectFirstLevelCacheManager: FirstLevelCacheManager<ComplexObject>
    {
        public ComplexObjectFirstLevelCacheManager(string defaultIndex = null)
            : base()
        {
            if (string.IsNullOrWhiteSpace(defaultIndex)) defaultIndex = DALSettingsWrapper.DefaultIndexName;
            var uri = DALSettingsWrapper.ElasticSearchEndpoint;
            CacheStore = new ElasticSearchCacheStore<ComplexObject>(new ComplexObjectElasticContext(uri, defaultIndex), uri, defaultIndex);
            PersistentRepository = new ComplexObjectRepository(new GoldfinchDbContext());
        }
    }
}
