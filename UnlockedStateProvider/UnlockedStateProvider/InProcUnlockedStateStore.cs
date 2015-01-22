using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UnlockedStateProvider
{
	public class InProcUnlockedStateStore : IUnlockedStateStore
	{
		private static readonly UnlockedStateStoreConfiguration configuration = UnlockedStateStoreConfiguration.Instance;
		public UnlockedStateStoreConfiguration Configuration { get { return configuration; } }
		public bool AutoSlidingSupport { get { return true; } }
		public bool AsyncSupport { get { return true; } }
		private Dictionary<string, object> _items;

		public Dictionary<string, object> Items
		{
			get
			{
				if (_items == null)
				{
					if (HttpContext.Current != null)
					{
						var store = HttpContext.Current.GetStoreFromCache(configuration.CookieName);
						if (store != null)
							_items = store.Items;
					}
				}
				return _items;
			}
			set
			{
				_items = value;
			}
		}

		public object this[string name]
		{
			get
			{
				var result = Items[name];
				return result;
			}
			set
			{
				Items[name] = value;
			}
		}

		public void Abondon(bool async = false)
		{
			var prefix = UnlockedExtensions.GetSessionItemKey(string.Empty, configuration.CookieName);

			UnlockedExtensions.EndSessionWithCustomCookie(configuration.CookieName);
		}

		public void ClearItems()
		{
			Items.Clear();
		}

		public void ClearAllItems(bool async = false)
		{
			throw new NotImplementedException();
		}

		public void SaveSession()
		{
			Set(UnlockedExtensions.UNLOCKED_STATE_STORE_KEY, Items);
		}

		public object Get(string key, bool slide = true, bool slideAsync = true)
		{
			if (configuration.ForceSlide) slide = true;
			var redisKey = GetSessionItemKey(key);
			var result = GetInternal(redisKey, slide, slideAsync);
			return result;
		}

		protected object GetInternal(string key, bool slide = true, bool slideAsync = true)
		{
			var data = HttpContext.Current.GetCacheItem(key);
			var result = data;
			if (slide)
			{
				var expire = UnlockedExtensions.GetNextTimeout(configuration.SessionTimeout);
				var prefix = GetSessionItemPrefix();
				SlideInternal(prefix, (int)expire.TotalSeconds, slideAsync);
			}
			return result;
		}

		public bool Set(string key, object value, TimeSpan? expireTime = null, bool async = false)
		{
			throw new NotImplementedException();
		}

		public bool Delete(string key, bool async = false)
		{
			throw new NotImplementedException();
		}

		public void Slide(bool async = false)
		{
			throw new NotImplementedException();
		}

		protected void SlideInternal(string prefix, int expireTime, bool async = false)
		{
			if (!prefix.StartsWith(UnlockedExtensions.UNLOCKED + ":")) return;
			var cache = HttpContext.Current.Cache.AsQueryable();
			
			
		}

		protected string GetSessionItemKey(string keyName)
		{
			var redisKey = UnlockedExtensions.GetSessionItemKey(keyName, configuration.CookieName);
			return redisKey;
		}

		protected string GetSessionItemPrefix()
		{
			var prefix = UnlockedExtensions.GetSessionItemKey(string.Empty, configuration.CookieName);
			return prefix;
		}
	}
}
