using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goldfinch
{
    public interface IPersistentRepository<TEntity>: IDisposable where TEntity: class
    {

        IQueryable<TEntity> AsQueryable();

        void Delete(object id, bool saveAfter = false, bool async = false);

        void Insert(TEntity entity, bool saveAfter = false, bool async = false);

        void Update(TEntity entity, bool saveAfter = false, bool async = false);

        void BulkInsert(IEnumerable<TEntity> entities, bool saveAfter = false, bool async = false);

        void BulkDelete(IEnumerable<object> ids, bool saveAfter = false, bool async = false);

        void BulkDelete(IEnumerable<TEntity> entities, bool saveAfter = false, bool async = false);

        void BulkUpdate(IEnumerable<TEntity> entities, bool saveAfter = false, bool async = false);

        TEntity GetById(object id);

        void Save(bool async = false);
    }
}
