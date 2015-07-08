using System;
using System.Collections.Generic;
using System.Linq;
using ElasticLinq;
using Nest;

namespace Goldfinch
{
    public class ElasticSearchCacheStore<TEntity> : IFirstLevelCacheStore<TEntity> where TEntity : class
    {
        private ElasticClient _client;
        private ElasticSearchContext _context;
        private string _nodeUrl;
        private string _defaultIndex;

        public ElasticSearchCacheStore(ElasticSearchContext context, string nodeUrl, string defaultIndex)
        {
            _nodeUrl = nodeUrl;
            _defaultIndex = defaultIndex;
            _context = context;
            var node = new Uri(_nodeUrl);
            var settings = new ConnectionSettings(node, _defaultIndex);
            _client = new ElasticClient(settings);
            try
            {
                Initialize();
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch { }
        }

        private int _retryOnConflict = 3;
        public int RetryOnConflict
        {
            get { return _retryOnConflict; }
            set { _retryOnConflict = value; }
        }

        public void Dispose() { }

        public IQueryable<TEntity> AsQueryable()
        {
            var queryable = _context.Query<TEntity>();
            return queryable;
        }

        public void Initialize()
        {
            var exists = _client.IndexExists(_defaultIndex);
            if (exists.Exists)
            {
                exists = _client.TypeExists(s => s.Type<TEntity>());
                if (!exists.Exists)
                    _client.Map<TEntity>(s => s.MapFromAttributes());
            }
            else
            {
                _client.CreateIndex(s => s.Index(_defaultIndex).AddMapping<TEntity>(t => t.MapFromAttributes()));
            }
        }

        public void Destroy()
        {
            var indexExists = _client.IndexExists(s => s.Index(_defaultIndex));
            if (!indexExists.Exists) return;
            _client.DeleteMapping<TEntity>(s => s.Index((_defaultIndex)));
        }

        public TEntity Get(object key)
        {
            var response = _client.Get<TEntity>(key.ToString());
            return response.Source;
        }

        public void Delete(object key)
        {
            _client.Delete<TEntity>(key.ToString());
        }

        public void BulkDelete(IEnumerable<object> keys)
        {
            if (keys == null || !keys.Any()) return;
            var skeys = keys.Cast<string>();
            var request = new DeleteByQueryRequest<TEntity>()
            {
                Query = new QueryContainer(new IdsQuery()
                {
                    Values = skeys
                })
            };
            _client.DeleteByQuery(request);
        }

        public void Add(TEntity entity)
        {
            _client.Index<TEntity>(entity);
        }

        public void BulkAdd(IEnumerable<TEntity> entities)
        {
            if (entities == null) return;
            _client.IndexMany<TEntity>(entities);
        }

        public void Update(TEntity entity)
        {
            _client.Update<TEntity>(d => d.Doc(entity).RetryOnConflict(RetryOnConflict).Refresh());
        }

        public void BulkUpdate(IEnumerable<TEntity> entities)
        {
            if (entities == null) return;
            var request = new BulkRequest() { Refresh = true };
            request.Operations = new List<IBulkOperation>(entities.Count());
            foreach (var entity in entities)
            {
                var op = new BulkUpdateOperation<TEntity, TEntity>(entity) { RetriesOnConflict = RetryOnConflict };
                request.Operations.Add(op);
            }
            _client.Bulk(request);
        }

        public void Clear()
        {
            _client.DeleteByQuery<TEntity>(q => q.MatchAll());
        }

        public void Fill(IEnumerable<TEntity> entities)
        {
            if (entities == null) return;
            int ps = 255; // Page Size
            int pc = entities.Count() / ps; // Page Count
            if (pc > 0)
            {
                for (var i = 0; i < pc + 1; i++)
                {
                    var page = entities.Skip(i * ps).Take(ps);
                    var request = new BulkRequest() { Refresh = true, };
                    request.Operations = new List<IBulkOperation>(ps);
                    foreach (var entity in page)
                    {
                        var op = new BulkUpdateOperation<TEntity, TEntity>(entity) { RetriesOnConflict = RetryOnConflict, DocAsUpsert = true };
                        request.Operations.Add(op);
                    }
                    if (request.Operations.Count > 0)
                        _client.Bulk(request);
                }
            }
            else
            {
                var request = new BulkRequest() { Refresh = true, };
                request.Operations = new List<IBulkOperation>(entities.Count());
                foreach (var entity in entities)
                {
                    var op = new BulkUpdateOperation<TEntity, TEntity>(entity) { RetriesOnConflict = RetryOnConflict, DocAsUpsert = true };
                    request.Operations.Add(op);
                }
                if (request.Operations.Count > 0)
                    _client.Bulk(request);
            }
        }

        public bool Any()
        {
            var response = _client.Search<TEntity>(s => s.MatchAll().Size(1));
            if (response.Total > 0) return true;
            return false;
        }
    }

}
