using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goldfinch
{
    public interface IFirstLevelCacheStore<TEntity>: IDisposable where TEntity : class
    {
        /// <summary>
        /// Call one time for initialization
        /// </summary>
        void Initialize();

        /// <summary>
        /// Call when you need to drop down containers
        /// </summary>
        void Destroy();

        /// <summary>
        /// To get IQueryable to write custom query
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> AsQueryable();

        /// <summary>
        /// Returns single object using FirstOrDefault
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TEntity Get(object key);

        /// <summary>
        /// Delete an item from store
        /// </summary>
        /// <param name="key"></param>
        void Delete(object key);

        /// <summary>
        /// Add an item to store
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// Update and item on store
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Clear all items from cachestore
        /// </summary>
        void Clear();


        /// <summary>
        /// Fill all items from persisten store to cache store
        /// </summary>
        void Fill(IEnumerable<TEntity> entities);

        bool Any();
    }
}
