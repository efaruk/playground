using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using StackExchange.Redis;

namespace UnlockedStateProvider.Redis
{
		public class RedisUnlockedStateStore : IUnlockedStateStore
		{
			private readonly IDatabase _redisDatabase;
			private readonly ConnectionMultiplexer _redisConnection;
			private readonly UnlockedStateStoreConfiguration _configuration = UnlockedStateStoreConfiguration.Instance;
			
			public RedisUnlockedStateStore()
			{
				var options = new ConfigurationOptions();
				options.EndPoints.Add(_configuration.Host, _configuration.Port);
				options.ConnectTimeout = _configuration.ConnectionTimeoutInMilliSec;
				options.SyncTimeout = _configuration.OperationTimeoutInMilliSec;
				options.ResolveDns = true;
				_redisConnection = ConnectionMultiplexer.Connect(options);
				_redisDatabase = _redisConnection.GetDatabase(int.Parse(_configuration.Database));
			}

			//public IUnlockedStateStore UnlockedStore()
			//{
			//	return new RedisUnlockedStateStore();
			//}

			//public void UpdateContext()
			//{
			//	SetContextItems(HttpContext.Current, _items);
			//}

			public UnlockedStateStoreConfiguration Configuration { get { return _configuration; } }

			public bool AutoSlidingSupport
			{
				get
				{
					return false;
				}
			}

			public bool AsyncSupport
			{
				get
				{
					return true;
				}
			}

			private Dictionary<string, object> _items;

			public Dictionary<string, object> Items
			{
				get
				{
					if (_items == null)
					{
						if (HttpContext.Current != null)
						{
							var store = HttpContext.Current.GetStoreFromContext();
							if (store != null)
								_items = store.Items;
						}
						// if (_items == null) _items = new Dictionary<string, object>(UnlockedStateUsageAttribute.DEFAULT_ITEM_COUNT);
					}
					return _items;
				}
				set
				{
					_items = value;
					// SetContextItems(HttpContext.Current, _items);
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
					//if (Items != null)
					//{
					//	Items[name] = value;
					//	//if (Items.ContainsKey(name))
					//	//	Items[name] = value;
					//	//else
					//	//	Items.Add(name, value);
					//}
					Items[name] = value;
				}
			}

			public void ClearItems()
			{
				Items.Clear();
			}

			public void ClearCustomItems(bool async = false)
			{
				string key = UnlockedExtensions.GetSessionKey(_configuration.CookieName);
				DeleteStartsWith(key, async);
			}

			//public IUnlockedStateStore GetStoreFromContext(HttpContextBase controllerContext)
			//{
			//	var store = (IUnlockedStateStore)controllerContext.GetContextItem(UnlockedStateUsageAttribute.UNLOCKED_STATE_STORE_KEY);
			//	return store;
			//}

			//public void SetContextItems(HttpContextBase controllerContext, Dictionary<string, object> items)
			//{
			//	controllerContext.SetContextItem(UnlockedStateUsageAttribute.UNLOCKED_STATE_OBJECT_KEY, items);
			//}

			//public void SetContextItems(HttpContext controllerContext, Dictionary<string, object> items)
			//{
			//	controllerContext.SetContextItem(UnlockedStateUsageAttribute.UNLOCKED_STATE_OBJECT_KEY, items);
			//}

			public object Get(string key, bool slide = true, bool slideAsync = true)
			{
				if (_configuration.ForceSlide) slide = true;
				var redisKey = GetSessionItemKey(key);
				var result = GetInternal(redisKey, slide, slideAsync);
				return result;
			}

			protected object GetInternal(string key, bool slide = true, bool slideAsync = true)
			{
				var data = _redisDatabase.StringGet(key);
				var result = StateBinarySerializer.Deserialize(data);
				if (slide)
				{
					var expire = DateTime.Now.AddMinutes(_configuration.SessionTimeout).TimeOfDay;
					Slide(key, expire, slideAsync);
				}
				return result;
			}

			public bool Set(string key, object value, TimeSpan expireTime, bool async = false)
			{
				var redisKey = GetSessionItemKey(key);
				var result = SetInternal(redisKey, value, expireTime, async);
				return result;
			}

			protected bool SetInternal(string key, object value, TimeSpan expireTime, bool async = false)
			{
				bool result = false;
				var data = (RedisValue)StateBinarySerializer.Serialize(value);
				if (async)
					_redisDatabase.StringSetAsync(key, data, expireTime);
				else
					result = _redisDatabase.StringSet(key, data, expireTime);
				return result;
			}

			public bool Delete(string key, bool async = false) {
				
				var redisKey = GetSessionItemKey(key);
				bool result = DeleteInternal(redisKey, async);
				return result;
			}

			protected bool DeleteInternal(string key, bool async = false)
			{
				bool result = false;
				if (async)
					_redisDatabase.KeyDeleteAsync(key);
				else
					result = _redisDatabase.KeyDelete(key);
				return result;
			}

			protected object Eval(string script, bool async = false) {
				object result = null; 
				if (async)
					_redisDatabase.ScriptEvaluateAsync(script);
				else
					result = _redisDatabase.ScriptEvaluate(script);
				return result;
			}

			protected void Slide(string prefix, TimeSpan expireTime, bool async = false)
			{
				string script = string.Format("\"return redis.call('expire', unpack(redis.call('keys', ARGV[1])), ARGV[2])\" 2 {0}:* {1}", prefix, expireTime);
				Eval(script, async);
			}

			public void DeleteStartsWith(string prefix, bool async = false)
			{
				string script = string.Format("\"return redis.call('del', unpack(redis.call('keys', ARGV[1])))\" 1 {0}:*", prefix);
				Eval(script, async);
			}

			private string GetSessionItemKey(string keyName)
			{
				var redisKey = UnlockedExtensions.GetSessionItemKey(keyName, _configuration.CookieName);
				return redisKey;
			}

			public void Dispose()
			{
				_redisConnection.Dispose();
				//_disposed = true;
			}

			//private bool _disposed = false;
			//public bool Disposed()
			//{
			//	return _disposed;
			//}

		}
}
