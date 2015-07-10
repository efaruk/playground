using System;
using System.Collections.Generic;
using System.Linq;
using ElasticLinq;
using Elasticsearch.Net;
using Nest;

namespace Goldfinch
{
    public class ElasticSearchCacheStore<TEntity> : IFirstLevelCacheStore<TEntity> where TEntity : class
    {
        #region Fields

        private ElasticClient _client;
        private ElasticSearchContext _context;
        private string _nodeUrl;
        private string _defaultIndex;

        #endregion

        #region Constructor

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

        #endregion

        #region Properties

        private int _retryOnConflict = 3;
        public int RetryOnConflict
        {
            get { return _retryOnConflict; }
            set { _retryOnConflict = value; }
        }

        #endregion

        #region Public

        public void Dispose() { }

        public IQueryable<TEntity> AsQueryable()
        {
            var queryable = _context.Query<TEntity>();
            return queryable;
        }

        public void Initialize()
        {
            var exists = _client.IndexExists(_defaultIndex);
            ValidateElasticSearchResponse(exists);
            if (exists.Exists)
            {
                exists = _client.TypeExists(s => s.Type<TEntity>());
                if (exists.Exists) return;
                var response = _client.Map<TEntity>(s => s.MapFromAttributes());
                ValidateElasticSearchResponse(response);
            }
            else
            {
                var response = _client.CreateIndex(s => s.Index(_defaultIndex).AddMapping<TEntity>(t => t.MapFromAttributes()));
                ValidateElasticSearchResponse(response);
            }
        }

        public void Destroy()
        {
            var indexExists = _client.IndexExists(s => s.Index(_defaultIndex));
            ValidateElasticSearchResponse(indexExists);
            if (!indexExists.Exists) return;
            var response = _client.DeleteMapping<TEntity>(s => s.Index((_defaultIndex)));
            ValidateElasticSearchResponse(response);
        }

        public TEntity Get(object key)
        {
            var response = _client.Get<TEntity>(key.ToString());
            ValidateElasticSearchResponse(response);
            return response.Source;
        }

        public void Delete(object key)
        {
            var response = _client.Delete<TEntity>(key.ToString());
            ValidateElasticSearchResponse(response);
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
            var response = _client.DeleteByQuery(request);
            ValidateElasticSearchResponse(response);
        }

        public void Add(TEntity entity)
        {
            var response = _client.Index<TEntity>(entity);
            ValidateElasticSearchResponse(response);
        }

        public void BulkAdd(IEnumerable<TEntity> entities)
        {
            if (entities == null) return;
            var response = _client.IndexMany<TEntity>(entities);
            ValidateElasticSearchResponse(response);
        }

        public void Update(TEntity entity)
        {
            var response = _client.Update<TEntity>(d => d.Doc(entity).RetryOnConflict(RetryOnConflict).Refresh());
            ValidateElasticSearchResponse(response);
        }

        public void BulkUpdate(IEnumerable<TEntity> entities)
        {
            if (entities == null) return;
            var request = new BulkRequest { Refresh = true, Operations = new List<IBulkOperation>(entities.Count()) };
            foreach (var entity in entities)
            {
                var op = new BulkUpdateOperation<TEntity, TEntity>(entity, entity, true) { RetriesOnConflict = RetryOnConflict, DocAsUpsert = true };
                request.Operations.Add(op);
            }
            var response = _client.Bulk(request);
            ValidateElasticSearchResponse(response);
        }

        public void Clear()
        {
            var response = _client.DeleteByQuery<TEntity>(q => q.MatchAll());
            ValidateElasticSearchResponse(response);
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
                    var request = new BulkRequest { Refresh = true, Operations = new List<IBulkOperation>(ps), };
                    foreach (var entity in page)
                    {
                        var op = new BulkUpdateOperation<TEntity, TEntity>(entity, entity, true) { RetriesOnConflict = RetryOnConflict, DocAsUpsert = true };
                        request.Operations.Add(op);
                    }
                    if (request.Operations.Count > 0)
                    {
                        var response = _client.Bulk(request);
                        ValidateElasticSearchResponse(response);
                    }
                }
            }
            else
            {
                var request = new BulkRequest { Refresh = true, Operations = new List<IBulkOperation>(entities.Count()), };
                foreach (var entity in entities)
                {
                    var op = new BulkUpdateOperation<TEntity, TEntity>(entity, entity, true) { RetriesOnConflict = RetryOnConflict, DocAsUpsert = true };
                    request.Operations.Add(op);
                }
                if (request.Operations.Count > 0)
                {
                    var response = _client.Bulk(request);
                    ValidateElasticSearchResponse(response);
                }
            }
        }

        public bool Any()
        {
            var response = _client.Search<TEntity>(s => s.MatchAll().Size(1));
            ValidateElasticSearchResponse(response);
            if (response.Total > 0) return true;
            return false;
        }

        #endregion

        #region Private

        private bool ValidateElasticSearchResponse(IResponse response, bool throwError = true)
        {
            bool rc = false;
            if (response == null) throw new ArgumentNullException("response");
            rc = response.IsValid;
            if (rc) return true;
            if (throwError)
                throw new ElasticsearchServerException(response.ServerError);
            return false;
        }
        #endregion
    }

}
