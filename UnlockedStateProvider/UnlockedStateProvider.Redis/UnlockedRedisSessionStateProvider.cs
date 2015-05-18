using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.SessionState;

namespace UnlockedStateProvider.Redis
{
    public class UnlockedRedisSessionStateProvider : SessionStateStoreProviderBase
    {
        private static readonly UnlockedStateStoreConfiguration Configuration = UnlockedStateStoreConfiguration.Instance;
        private static readonly IUnlockedStateStore Store = new RedisUnlockedStateStore();

        static UnlockedRedisSessionStateProvider()
        {
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null || !config.HasKeys())
                throw new ArgumentNullException("config");
            name = "UnlockedStateProvider";
            base.Initialize(name, config);
            if (!Configuration.ConfiguredAsStandardProvider)
            {
                lock (Configuration.ConfigurationCreationLock)
                {
                    Configuration.ConfigureAsSdandardProvider(config);
                }
            }
        }

        public override SessionStateStoreData CreateNewStoreData(System.Web.HttpContext context, int timeout)
        {
            return new SessionStateStoreData(new SessionStateItemCollection(), new HttpStaticObjectsCollection(), timeout);
        }

        public override void CreateUninitializedItem(System.Web.HttpContext context, string id, int timeout)
        {
            if (Store.Items == null)
                Store.Items = new Dictionary<string, object>(UnlockedExtensions.DEFAULT_ITEM_COUNT);
        }

        public override void Dispose()
        {
        }

        public override void EndRequest(System.Web.HttpContext context)
        {
            if (UnlockedStateStoreConfiguration.Instance.Disabled) return;

            //var session = filterContext.HttpContext.GetContextItem(UNLOCKED_STATE_OBJECT_KEY);
            if (Store.Items.Count > 0)
            {
                //filterContext.StartSessionIfNew();
                //var store = (IUnlockedStateStore)filterContext.GetContextItem(UNLOCKED_STATE_STORE_KEY);
                // store.UpdateContext();
                var expire = UnlockedExtensions.GetNextTimeout(Store.Configuration.SessionTimeout);
                Store.Set(UnlockedExtensions.UNLOCKED_STATE_STORE_KEY, Store.Items, expire, RunAsync);
            }
            //base.EndRequest(context);
            //UnlockedStateStore.Dispose();
        }

        public override SessionStateStoreData GetItem(System.Web.HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
        {
            throw new NotImplementedException();
        }

        public override SessionStateStoreData GetItemExclusive(System.Web.HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
        {
            throw new NotImplementedException();
        }

        public override void InitializeRequest(System.Web.HttpContext context)
        {
            throw new NotImplementedException();
        }

        public override void ReleaseItemExclusive(System.Web.HttpContext context, string id, object lockId)
        {
            throw new NotImplementedException();
        }

        public override void RemoveItem(System.Web.HttpContext context, string id, object lockId, SessionStateStoreData item)
        {
            throw new NotImplementedException();
        }

        public override void ResetItemTimeout(System.Web.HttpContext context, string id)
        {
            throw new NotImplementedException();
        }

        public override void SetAndReleaseItemExclusive(System.Web.HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
        {
            throw new NotImplementedException();
        }

        public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
        {
            throw new NotImplementedException();
        }
    }
}
