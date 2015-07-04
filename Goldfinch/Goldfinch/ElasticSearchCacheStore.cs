using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticLinq;
using Elasticsearch.Net;
using Nest;

namespace Goldfinch
{
    public class ElasticSearchCacheStore<TEntity>: IFirstLevelCacheStore<TEntity> where TEntity: class
    {
        private ElasticClient _client;
        private ElasticContext _context;
        private string _nodeUrl;
        private string _defaultIndex;
        //private const string QueryStringFormat = "{\"query\": \"query_string\" : {\"fields\" : [\"{0}\"], \"query\" : \"{1}\"}}";
        //private const string GetQueryStringFormat = "\"query\": {\"query_string\" : {\"default_field\" : [\"_id\"], \"query\" : \"{0}\"}}";

        public ElasticSearchCacheStore(ElasticContext context, string nodeUrl, string defaultIndex)
        {
            _nodeUrl = nodeUrl;
            _defaultIndex = defaultIndex;
            _context = context;
            var node = new Uri(_nodeUrl);
            var settings = new ConnectionSettings(node, _defaultIndex);
            _client = new ElasticClient(settings);
        }

        private long _retryOnConflict = 3;
        public long RetryOnConflict
        {
            get { return _retryOnConflict; }
            set { _retryOnConflict = value; }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _context.Query<TEntity>();
        }

        public void Initialize()
        {
            _client.CreateIndex(s => s.Index(_defaultIndex).AddMapping<TEntity>(t => t.MapFromAttributes()));
            //_client.Map<TEntity>(d => d.MapFromAttributes());
        }

        public TEntity Get(string key)
        {
            var response = _client.Get<TEntity>(key);
            return response.Source;
        }

        public void Delete(string key)
        {
            _client.Delete<TEntity>(key, s => s);
        }

        public void Add(TEntity data)
        {
            _client.Index<TEntity>(data, s => s.IdFrom(data));
        }

        public void Update(TEntity data)
        {
            _client.Update<TEntity>(d => d.IdFrom(data).Doc(data).RetryOnConflict(RetryOnConflict).Refresh());
        }
    }
}
