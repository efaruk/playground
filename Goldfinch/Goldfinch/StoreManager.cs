using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Goldfinch
{
    public class StoreManager<TEntity>: IFirstLevelCacheStore<TEntity> where TEntity: class 
    {
        protected GenericRepository<TEntity> PersistentRepository;
        protected IFirstLevelCacheStore<TEntity> CacheStore;

        protected StoreManager()
        {
        }

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

        public void Dispose()
        {
            PersistentRepository.Dispose();
        }

        public void Initialize()
        {
            CacheStore.Initialize();
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return CacheStore.AsQueryable();
        }

        public TEntity Get(string key)
        {
            return CacheStore.Get(key);
        }

        public void Delete(string key)
        {
            using (var scope = new TransactionScope())
            {
                PersistentRepository.Delete(key, true);
                CacheStore.Delete(key);
                scope.Complete();
            }
        }

        public void Add(TEntity data)
        {
            using (var scope = new TransactionScope())
            {
                PersistentRepository.Insert(data, true);
                CacheStore.Add(data);
                scope.Complete();
            }
        }

        public void Update(TEntity data)
        {
            using (var scope = new TransactionScope())
            {
                PersistentRepository.Update(data, true);
                CacheStore.Update(data);
                scope.Complete();
            }
        }
    }
}
