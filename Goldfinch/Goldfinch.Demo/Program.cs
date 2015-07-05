using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goldfinch.Demo.DAL;
using Goldfinch.Demo.Data;

namespace Goldfinch.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Clean cache strore
            CleanCacheStore();
            // Clean Database
            using (var context = new GoldfinchContext())
            {
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "DELETE FROM SimpleItem; VACUUM;");
            }
            //FillDatabase();
            //FillCacheStore();
            RealWorldScenario();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void RealWorldScenario()
        {
            var maxItemCount = 1000;
            using (var manager = new SimpleItemStoreManager())
            {
                for (int i = 0; i < maxItemCount; i++)
                {
                    var entity = new SimpleItem()
                    {
                        ContainerName = "TestContainer",
                        FieldName = "TestField",
                        FieldValue = "Test"
                    };
                    manager.Add(entity);
                }

                for (int i = 0; i < maxItemCount; i++)
                {
                    var entity = new SimpleItem()
                    {
                        PkId = i + 1,
                        ContainerName = "TestContainer" + i,
                        FieldName = "TestField" + i,
                        FieldValue = "Test" + i
                    };
                    manager.Update(entity);
                }

                manager.Refresh(true);


                var rnd = new Random();
                for (int i = 0; i < maxItemCount / 10; i++)
                {
                    var t = rnd.Next(maxItemCount);
                    try
                    {
                        manager.Delete(i);
                    }
                    // ReSharper disable once EmptyGeneralCatchClause
                    catch { }
                }

                var result = manager.AsQueryable().ToList();
                foreach (var simpleItem in result)
                {
                    Console.WriteLine("PkId: {0}, ContainerName: {1}, FieldName: {2}, FieldValue: {3}", simpleItem.PkId, simpleItem.ContainerName, simpleItem.FieldName, simpleItem.FieldValue);
                }
            }
        }

        private static void FillDatabase()
        {
            using (var repository = new SimpleItemRepository())
            {
                for (int i = 0; i < 1000; i++)
                {
                    var entity = new SimpleItem()
                    {
                        ContainerName = "TestContainer",
                        FieldName = "TestField",
                        FieldValue = "Test"
                    };
                    repository.Insert(entity, true);
                }
            }
        }

        private static void FillCacheStore()
        {
            using (var store = new ElasticSearchCacheStore<SimpleItem>(new SimpleItemElasticContext(), DALSettingsWrapper.ElasticSearchEndpoint, "goldfinch"))
            {
                store.Initialize();
                for (int i = 0; i < 1000; i++)
                {
                    var entity = new SimpleItem()
                    {
                        PkId = i + 1,
                        ContainerName = "TestContainer",
                        FieldName = "TestField",
                        FieldValue = "Test"
                    };
                    store.Add(entity);
                }
            }
        }

        private static void CleanCacheStore()
        {
            using (var store = new ElasticSearchCacheStore<SimpleItem>(new SimpleItemElasticContext(), DALSettingsWrapper.ElasticSearchEndpoint, "goldfinch"))
            {
                store.Clear();
            }
        }
    }
}
