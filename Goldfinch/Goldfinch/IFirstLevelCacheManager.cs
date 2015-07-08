using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goldfinch
{
    public interface IFirstLevelCacheManager<TEntity>: IDisposable where TEntity: class 
    {
        /// <summary>
        /// Returns IQueryable<TEntity> only from Cache Store
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> AsQueryable();

        /// <summary>
        /// Get an item only from Cache Store
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TEntity Get(object key);

        /// <summary>
        /// Delete an item from both Persistent Repository and Cache Store
        /// </summary>
        /// <param name="key"></param>
        void Delete(object key);

        /// <summary>
        /// Delete list of items from both Persistent Repository and Cache Store
        /// </summary>
        /// <param name="key"></param>
        void BulkDelete(IEnumerable<object> keys);

        /// <summary>
        /// Add an item on both Persistent Repository and Cache Store
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// Add list of items on both Persistent Repository and Cache Store
        /// </summary>
        /// <param name="entity"></param>
        void BulkAdd(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update an item on both Persistent Repository and Cache Store
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Update list of items on both Persistent Repository and Cache Store
        /// </summary>
        /// <param name="entity"></param>
        void BulkUpdate(IEnumerable<TEntity> entities);

        /// <summary>
        /// Clear Cache store
        /// </summary>
        void Clear();

        /// <summary>
        /// Fill cache store with given entities or all entities from Persistent Repository
        /// </summary>
        /// <param name="entities"></param>
        void Fill(IEnumerable<TEntity> entities = null);

        /// <summary>
        /// Refill cache store if there is no item or force set true
        /// </summary>
        /// <param name="force"></param>
        void Refresh(bool force = false);
    }
}
