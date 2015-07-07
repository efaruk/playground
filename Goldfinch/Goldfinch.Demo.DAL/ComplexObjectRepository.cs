using System;
using System.Collections.Generic;
using System.Data.Entity;
using Goldfinch.Demo.Data;

namespace Goldfinch.Demo.DAL
{
    public class ComplexObjectRepository : GenericRepository<ComplexObject>, IPersistentRepository<ComplexObject>
    {
        public ComplexObjectRepository() : base(new GoldfinchDbContext())
        {
            
        }

        public ComplexObjectRepository(DbContext context)
            : base(context, true, true, true, true)
		{
		}

    }
}
