using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Goldfinch
{
    public class FirstLevelCacheManager<TEntity> : IFirstLevelCacheManager<TEntity> where TEntity : class
    {
        protected IPersistentRepository<TEntity> PersistentRepository;
        protected IFirstLevelCacheStore<TEntity> CacheStore;

        /// <summary>
        /// You have to set PersistentRepository and CacheStore before use
        /// </summary>
        protected FirstLevelCacheManager()
        {
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="persistentRepository"></param>
        /// <param name="cacheStore"></param>
        public FirstLevelCacheManager(IPersistentRepository<TEntity> persistentRepository, IFirstLevelCacheStore<TEntity> cacheStore)
        {
            PersistentRepository = persistentRepository;
            CacheStore = cacheStore;
        }

        /// <summary>
        /// Dispose Persistent Repository
        /// </summary>
        public void Dispose()
        {
            PersistentRepository.Dispose();
        }

        /// <summary>
        /// Returns IQueryable<TEntity> only from Cache Store
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> AsQueryable()
        {
            return CacheStore.AsQueryable();
        }

        /// <summary>
        /// Get an item only from Cache Store
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TEntity Get(object key)
        {
            return CacheStore.Get(key);
        }

        /// <summary>
        /// Delete an item from both Persistent Repository and Cache Store
        /// </summary>
        /// <param name="key"></param>
        public void Delete(object key)
        {
            using (var scope = new TransactionScope())
            {
                PersistentRepository.Delete(key, true);
                CacheStore.Delete(key);
                scope.Complete();
            }
        }

        public void BulkDelete(IEnumerable<object> keys)
        {
            using (var scope = new TransactionScope())
            {
                PersistentRepository.BulkDelete(keys, true);
                CacheStore.BulkDelete(keys);
                scope.Complete();
            }
        }

        /// <summary>
        /// Add an item on both Persistent Repository and Cache Store
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TEntity entity)
        {
            using (var scope = new TransactionScope())
            {
                PersistentRepository.Insert(entity, true);
                CacheStore.Add(entity);
                scope.Complete();
            }
        }

        public void BulkAdd(IEnumerable<TEntity> entities)
        {
            using (var scope = new TransactionScope())
            {
                PersistentRepository.BulkInsert(entities, true);
                CacheStore.BulkAdd(entities);
                scope.Complete();
            }
        }

        /// <summary>
        /// Update an item on both Persistent Repository and Cache Store
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            using (var scope = new TransactionScope())
            {
                PersistentRepository.Update(entity, true);
                CacheStore.Update(entity);
                scope.Complete();
            }
        }

        public void BulkUpdate(IEnumerable<TEntity> entities)
        {
            using (var scope = new TransactionScope())
            {
                PersistentRepository.BulkUpdate(entities, true);
                CacheStore.BulkUpdate(entities);
                scope.Complete();
            }
        }

        /// <summary>
        /// Clear Cache store
        /// </summary>
        public void Clear()
        {
            CacheStore.Clear();
        }

        /// <summary>
        /// Fill cache store with given entities or all entities from Persistent Repository
        /// </summary>
        /// <param name="entities"></param>
        public void Fill(IEnumerable<TEntity> entities = null)
        {
            if (entities == null)
            {
                entities = PersistentRepository.AsQueryable().ToList();
            }
            CacheStore.Fill(entities);
        }

        /// <summary>
        /// Refill cache store if there is no item or force set true
        /// </summary>
        /// <param name="force"></param>
        public void Refresh(bool force = false)
        {
            if (CacheStore.Any())
            {
                if (!force) return;
                Clear();
                Fill();
            }
            else
            {
                Fill();
            }
        }

    }
}
