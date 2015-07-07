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
        private const int MaxItemCount = 100;

        static void Main(string[] args)
        {
            // Clean cache strore
            CleanCacheStore();
            // Clean Database
            CleanDatabase();
            //FillDatabase();
            //FillCacheStore();
            RealWorldScenario();
            RealWorldScenarioComplexObject();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void RealWorldScenarioComplexObject()
        {
            var rnd = new Random();
            using (var manager = new ComplexObjectFirstLevelCacheManager())
            {
                for (int i = 0; i < MaxItemCount; i++)
                {
                    var entity = new ComplexObject()
                    {
                        Name = "Complex Object",
                        Amount = (decimal)rnd.NextDouble() * MaxItemCount,
                        Data = new byte[]{0, 1, 2, 3},
                        IsCrew = false,
                    };
                    manager.Add(entity);
                }

                for (int i = 0; i < MaxItemCount; i++)
                {
                    var entity = new ComplexObject()
                    {
                        PkId = i + 1,
                        Name = "Complex Object " + i,
                        Amount = (decimal)rnd.NextDouble() * MaxItemCount,
                        Data = new byte[] { 3, 2, 1, 0 },
                        IsCrew = true,
                    };
                    manager.Update(entity);
                }

                //manager.Refresh(true);
                
                for (int i = 0; i < MaxItemCount / 10; i++)
                {
                    var t = rnd.Next(MaxItemCount);
                    try
                    {
                        manager.Delete(i);
                    }
                    // ReSharper disable once EmptyGeneralCatchClause
                    catch { }
                }
                var result = manager.AsQueryable().Take(MaxItemCount).ToList();
                foreach (var item in result)
                {
                    Console.WriteLine("PkId: {0}, Name: {1}, Modified: {2}, Amount: {3}, IsCrew: {4}, Data: {5}", item.PkId, item.Name, item.ModifiedDate, item.Amount, item.IsCrew, item.Data);
                }
            }
        }

        private static void RealWorldScenario()
        {
            var rnd = new Random();
            using (var manager = new SimpleItemFirstLevelCacheManager())
            {
                for (int i = 0; i < MaxItemCount; i++)
                {
                    var entity = new SimpleItem()
                    {
                        ContainerName = "TestContainer",
                        FieldName = "TestField",
                        FieldValue = "Test"
                    };
                    manager.Add(entity);
                }

                for (int i = 0; i < MaxItemCount; i++)
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

                //manager.Refresh(true);

                
                for (int i = 0; i < MaxItemCount / 10; i++)
                {
                    var t = rnd.Next(MaxItemCount);
                    try
                    {
                        manager.Delete(i);
                    }
                    // ReSharper disable once EmptyGeneralCatchClause
                    catch { }
                }

                var result = manager.AsQueryable().Take(MaxItemCount).ToList();
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
            using (var store = new ElasticSearchCacheStore<SimpleItem>(new SimpleItemElasticContext(), DALSettingsWrapper.ElasticSearchEndpoint, DALSettingsWrapper.DefaultIndexName))
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

        private static void CleanDatabase()
        {
            using (var context = new GoldfinchDbContext())
            {
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "DELETE FROM SimpleItem; DELETE FROM ComplexObject; VACUUM;");
            }
        }

        private static void CleanCacheStore()
        {
            using (var store = new ElasticSearchCacheStore<SimpleItem>(new SimpleItemElasticContext(), DALSettingsWrapper.ElasticSearchEndpoint, DALSettingsWrapper.DefaultIndexName))
            {
                store.Clear();
            }
            using (var store = new ElasticSearchCacheStore<ComplexObject>(new ComplexObjectElasticContext(), DALSettingsWrapper.ElasticSearchEndpoint, DALSettingsWrapper.DefaultIndexName))
            {
                store.Clear();
            }
        }
    }
}
