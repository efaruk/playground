using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goldfinch
{
    /// <summary>
    /// Entity Framework helper
    /// </summary>
    public static class EntityFrameworkHelper
    {
        public static object GetPrimaryKey<TEntity>(DbContext context, TEntity entity) where TEntity : class
        {
            object result = null;
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var set = ((IObjectContextAdapter)context).ObjectContext.CreateObjectSet<TEntity>();
            var entitySet = set.EntitySet;
            var propertyName = entitySet.ElementType.KeyMembers.Select(k => k.Name).FirstOrDefault();
            if (propertyName != null)
            {
                result = entity.GetType().GetProperty(propertyName).GetValue(entity);
            }
            return result;
        }

    }
}
