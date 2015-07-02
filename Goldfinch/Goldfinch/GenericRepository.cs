using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goldfinch
{
    public class GenericRepository<TEntity> where TEntity: class 
    {
        private DbContext Context;
        private DbSet<TEntity> Set;

        public const int DEFAULT_MAX_ENTITY_COUNT = 1000;
        public const int DEFAULT_ENTITY_COUNT = 10;
        public const int DEFAULT_COMMAND_TIMEOUT = 30;

        private GenericRepository()
        {
            
        }

        public GenericRepository(DbContext context, bool disableChangeTracking = false, bool disableLazyLoading = false, bool disableProxyCreation = false, bool useDatabaseNullSemantics = false, bool disableValidateOnSave = false)
        {
            Context = context;
            Set = context.Set<TEntity>();

            Context.Configuration.AutoDetectChangesEnabled = !disableChangeTracking;
            Context.Configuration.LazyLoadingEnabled = !disableLazyLoading;
            Context.Configuration.ProxyCreationEnabled = !disableProxyCreation;
            Context.Configuration.UseDatabaseNullSemantics = !useDatabaseNullSemantics;
            Context.Configuration.ValidateOnSaveEnabled = !disableValidateOnSave;
        }

        public T GetContext<T>()
        {
            return (T)(object)Context;
        }

        public DbContextConfiguration ContextConfiguration
        {
            get { return this.Context.Configuration; }
        }

        /// <summary>
        /// Return TEntity as IQuearyable
        /// </summary>
        public IQueryable<TEntity> AsQueryable()
        {
            return Set.AsQueryable();
        }

        /// <summary>
        /// Get TEntity as IQuearyable
        /// </summary>
        public IQueryable<TEntity> Entities
        {
            get { return Set.AsQueryable(); }
        }


    }
}
