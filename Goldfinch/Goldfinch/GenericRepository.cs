using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goldfinch
{
    public class GenericRepository<TEntity> : IDisposable where TEntity : class
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

        #region IDisposable

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        public DbContext GetContext()
        {
            return Context;
        }

        #region Public

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

        public virtual TEntity GetById(object id)
        {
            return Set.Find(id);
        }

        public virtual TEntity GetById(long id)
        {
            return Set.Find(id);
        }

        public virtual void Insert(TEntity entity, bool saveAfter = false, bool async = false)
        {
            Set.Add(entity);
            if (saveAfter) Save(async);
            var primaryKey = RuntimeHelper.GetPrimaryKey(Context, entity);
            DataAdded(primaryKey, entity);
        }

        public virtual void BulkInsert(IEnumerable<TEntity> entities, bool saveAfter = false, bool async = false)
        {
            foreach (var entity in entities)
            {
                Set.Add(entity);
            }
            if (saveAfter) Save(async);
            foreach (var entity in entities)
            {
                var primaryKey = RuntimeHelper.GetPrimaryKey(Context, entity);
                DataAdded(primaryKey, entity);
            }
        }

        public virtual void Delete(object id, bool saveAfter = false, bool async = false)
        {
            var entityToDelete = Set.Find(id);
            Delete(entityToDelete);
            if (saveAfter) Save(async);
            DataDeleted(id);
        }

        public virtual void BulkDelete(IEnumerable<object> ids, bool saveAfter = false, bool async = false)
        {
            foreach (var id in ids)
            {
                var entityToDelete = Set.Find(id);
                Delete(entityToDelete);
            }
            if (saveAfter) Save(async);
            foreach (var id in ids)
            {
                DataDeleted(id);
            }
        }

        public virtual void Delete(TEntity entity, bool saveAfter = false, bool async = false)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Set.Attach(entity);
            }
            Set.Remove(entity);
            if (saveAfter) Save(async);
            var primaryKey = RuntimeHelper.GetPrimaryKey(Context, entity);
            DataDeleted(primaryKey);
        }

        public virtual void BulkDelete(IEnumerable<TEntity> entities, bool saveAfter = false, bool async = false)
        {
            foreach (var entity in entities)
            {
                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    Set.Attach(entity);
                }
                Set.Remove(entity);
            }
            if (saveAfter) Save(async);
            foreach (var entity in entities)
            {
                var primaryKey = RuntimeHelper.GetPrimaryKey(Context, entity);
                DataDeleted(primaryKey);
            }
        }

        public virtual void Update(TEntity entity, bool saveAfter = false, bool async = false)
        {
            Set.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            if (saveAfter) Save(async);
            var primaryKey = RuntimeHelper.GetPrimaryKey(Context, entity);
            DataUpdated(primaryKey, entity);
        }

        public virtual void BulkUpdate(IEnumerable<TEntity> entities, bool saveAfter = false, bool async = false)
        {
            foreach (var entity in entities)
            {
                Set.Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
            }
            if (saveAfter) Save(async);
            foreach (var entity in entities)
            {
                var primaryKey = RuntimeHelper.GetPrimaryKey(Context, entity);
                DataUpdated(primaryKey, entity);
            }
        }

        public virtual void Save(bool async = false)
        {
            if (async)
            {
                Context.SaveChangesAsync();
            }
            else
            {
                Context.SaveChanges();
            }
        }

        #endregion


        #region Events

        public virtual event DataDeleted OnDataDeleted;

        protected virtual void DataDeleted(object key)
        {
            DataDeleted handler = OnDataDeleted;
            if (handler != null) handler(key);
        }

        public virtual event DataAdded OnDataAdded;

        protected virtual void DataAdded(object key, object data)
        {
            DataAdded handler = OnDataAdded;
            if (handler != null) handler(key, data);
        }

        public virtual event DataUpdated OnDataUpdated;

        protected virtual void DataUpdated(object key, object data)
        {
            DataUpdated handler = OnDataUpdated;
            if (handler != null) handler(key, data);
        }

        #endregion
    }
}
