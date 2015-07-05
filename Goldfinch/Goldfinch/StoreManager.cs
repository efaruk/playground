using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Goldfinch
{
    public class StoreManager<TEntity> : IDisposable where TEntity : class
    {
        protected GenericRepository<TEntity> PersistentRepository;
        protected IFirstLevelCacheStore<TEntity> CacheStore;

        /// <summary>
        /// You have to set PersistentRepository and CacheStore and Initialize CacheStore before use
        /// </summary>
        protected StoreManager()
        {
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="persistentRepository"></param>
        /// <param name="cacheStore"></param>
        public StoreManager(GenericRepository<TEntity> persistentRepository, IFirstLevelCacheStore<TEntity> cacheStore)
        {
            PersistentRepository = persistentRepository;
            CacheStore = cacheStore;
            try
            {
                Initialize();
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch //Swallow the exception on first Initialize(), Index can be created before instance; 
            { }
        }

        /// <summary>
        /// Dispose Persistent Repository
        /// </summary>
        public void Dispose()
        {
            PersistentRepository.Dispose();
        }

        /// <summary>
        /// Initialize Cache Store
        /// </summary>
        public void Initialize()
        {
            CacheStore.Initialize();
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
        /// Refill cache store if there is no item or forceToReFill set true
        /// </summary>
        /// <param name="forceToReFill"></param>
        public void Refresh(bool forceToReFill = false)
        {
            if (!CacheStore.Any() || forceToReFill)
            {
                Clear();
                Fill();
            }
        }
    }
}
