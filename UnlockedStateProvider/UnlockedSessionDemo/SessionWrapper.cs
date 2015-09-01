using System;
using System.Collections.Generic;
using System.Web;
using UnlockedStateProvider;

namespace UnlockedSessionDemo
{
    public static class SessionWrapper
    {
        public static IUnlockedStateStore UnlockedStore
        {
            get
            {
                var store = HttpContext.Current.GetStoreFromContext();
                return store;
            }
        }

        public static void Abondon()
        {
            UnlockedStore.Abondon();
        }

        public static Dictionary<string, object> UnlockedItems
        {
            get
            {
                var items = HttpContext.Current.GetStoreFromContext().Items;
                return items;
            }
        }

        public static string Test
        {
            get
            {
                var item = UnlockedItems["test"];
                return (string) item;
            }
            set { UnlockedItems["test"] = value; }
        }

        public static double Double
        {
            get
            {
                var item = UnlockedItems["double"];
                return (double) item;
            }
            set { UnlockedItems["double"] = value; }
        }

        public static DateTime Date
        {
            get
            {
                var item = UnlockedItems["date"];
                return (DateTime) item;
            }
            set { UnlockedItems["date"] = value; }
        }

        public static CustomSessionObject CustomSessionObject
        {
            get
            {
                var item = UnlockedItems["custom"] ?? new CustomSessionObject();
                return (CustomSessionObject) item;
            }
            set { UnlockedItems["custom"] = value; }
        }

        public static BigSessionObject BigSessionObject
        {
            get
            {
                var item = UnlockedItems["big"] ?? new BigSessionObject();
                return (BigSessionObject) item;
            }
            set { UnlockedItems["big"] = value; }
        }

        public static void SetCustom(string key, object value, TimeSpan? expire = null, bool async = true)
        {
            UnlockedStore.Set(key, value, expire, async);
        }

        public static object GetCustom(string key, bool slide = true, bool slideAsync = true)
        {
            var value = UnlockedStore.Get(key, slide, slideAsync);
            return value;
        }
    }
}