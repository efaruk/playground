using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goldfinch.Demo.Data
{
    public class GoldfinchDbContext: DbContext
    {
        public GoldfinchDbContext()
        {
            //this.Database.CommandTimeout = 60;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<SimpleItem> SimpleItems { get; set; }
        public DbSet<ComplexObject> ComplexObjects { get; set; }

    }
}
