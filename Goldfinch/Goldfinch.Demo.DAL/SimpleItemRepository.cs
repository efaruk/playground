using System;
using System.Collections.Generic;
using System.Data.Entity;
using Goldfinch.Demo.Data;

namespace Goldfinch.Demo.DAL
{
    public class SimpleItemRepository : GenericRepository<SimpleItem>, IPersistentRepository<SimpleItem>
    {
        public SimpleItemRepository() : base(new GoldfinchContext())
        {
            
        }

        public SimpleItemRepository(DbContext context) : base(context, true, true, true, true)
		{
		}

    }
}
