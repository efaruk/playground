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
        /// To get IQueryable to write custom query
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> AsQueryable();

        /// <summary>
        /// Returns single object using FirstOrDefault
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TEntity Get(string key);

        /// <summary>
        /// Delete an item from store
        /// </summary>
        /// <param name="key"></param>
        void Delete(string key);

        /// <summary>
        /// Add an item to store
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="timeSpan"></param>
        void Add(TEntity data);

        /// <summary>
        /// Update and item on store
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="timeSpan"></param>
        void Update(TEntity data);
    }
}
