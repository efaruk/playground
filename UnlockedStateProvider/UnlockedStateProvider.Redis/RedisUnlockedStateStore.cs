using System;
using System.Collections.Generic;
using System.Web;
using StackExchange.Redis;

namespace UnlockedStateProvider.Redis
{
		public class RedisUnlockedStateStore : IUnlockedStateStore
		{
			private const char SPLITTER = ',';
			//private const int DEFAULT_PORT = 6379;
			//private readonly IDatabase _redisDatabase;
			private static readonly Lazy<ConnectionMultiplexer> LazyConnection = new Lazy<ConnectionMultiplexer>(() =>
			{
				var connection = ConnectWithConfiguration();
				return connection;
			});

			private static ConnectionMultiplexer RedisConnection
			{
				get { return LazyConnection.Value; }
			}

			private static readonly UnlockedStateStoreConfiguration configuration = UnlockedStateStoreConfiguration.Instance;

			//public RedisUnlockedStateStore()
			//{
			//	//_redisConnection = ConnectWithConfiguration();
			//	//_redisDatabase = RedisConnection.GetDatabase(int.Parse(configuration.Database));
			//}

			private static ConnectionMultiplexer ConnectWithConfiguration()
			{
				var options = new ConfigurationOptions
				{
					ClientName = configuration.ApplicationName,
					ConnectTimeout = configuration.ConnectionTimeoutInMilliSec,
					SyncTimeout = configuration.OperationTimeoutInMilliSec,
					ResolveDns = true,
					AbortOnConnectFail = false // Important for shared usage
				};
				if (string.IsNullOrWhiteSpace(configuration.AccessKey))
					options.Password = configuration.AccessKey;
				if (configuration.RetryCount > 0)
					options.ConnectRetry = configuration.RetryCount;
				if (configuration.UseSsl)
				{
					options.Ssl = configuration.UseSsl;
					options.SslHost = configuration.Host;
				}
				var hosts = configuration.Host.Split(new[] { SPLITTER }, StringSplitOptions.RemoveEmptyEntries);
				foreach (var host in hosts)
				{
					options.EndPoints.Add(host);
				}
				//options.EndPoints.Add(configuration.Host, configuration.Port);
				var redisConnection = ConnectionMultiplexer.Connect(options);
				return redisConnection;
			}

			private IDatabase GetRedisDatabase()
			{
				var database = RedisConnection.GetDatabase(int.Parse(configuration.Database));
				return database;
			}

			//public IUnlockedStateStore UnlockedStore()
			//{
			//	return new RedisUnlockedStateStore();
			//}

			//public void UpdateContext()
			//{
			//	SetContextItems(HttpContext.Current, _items);
			//}

			public UnlockedStateStoreConfiguration Configuration { get { return configuration; } }

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

			public void Abondon(bool async = false)
			{
				var key = GetSessionItemPrefix();
				DeleteStartsWith(key, async);
				UnlockedExtensions.EndSessionWithCustomCookie(configuration.CookieName);
			}

			public void ClearItems()
			{
				Items.Clear();
			}

			public void ClearAllItems(bool async = false)
			{
				Items.Clear();
				var key = GetSessionItemPrefix();
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
				if (configuration.ForceSlide) slide = true;
				var redisKey = GetSessionItemKey(key);
				var result = GetInternal(redisKey, slide, slideAsync);
				return result;
			}

			protected object GetInternal(string key, bool slide = true, bool slideAsync = true)
			{
				var redisDatabase = GetRedisDatabase();
				var data = redisDatabase.StringGet(key);
				var result = StateBinarySerializer.Deserialize(data);
				if (slide)
				{
					var expire = UnlockedExtensions.GetNextTimeout(configuration.SessionTimeout);
					var prefix = GetSessionItemPrefix();
					SlideInternal(prefix, expire, slideAsync);
				}
				return result;
			}

			public bool Set(string key, object value, TimeSpan? expireTime = null, bool async = false)
			{
				var expire = expireTime.HasValue ? expireTime.Value : UnlockedExtensions.GetNextTimeout(configuration.SessionTimeout);
				var redisKey = GetSessionItemKey(key);
				var result = SetInternal(redisKey, value, expire, async);
				return result;
			}

			protected bool SetInternal(string key, object value, TimeSpan expireTime, bool async = false)
			{
				bool result = false;
				var data = (RedisValue)StateBinarySerializer.Serialize(value);
				var redisDatabase = GetRedisDatabase();
				if (async)
					redisDatabase.StringSetAsync(key, data, expireTime);
				else
					result = redisDatabase.StringSet(key, data, expireTime);
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
				var redisDatabase = GetRedisDatabase();
				if (async)
					redisDatabase.KeyDeleteAsync(key);
				else
					result = redisDatabase.KeyDelete(key);
				return result;
			}

			protected object Eval(string script, bool async = false, CommandFlags flags = CommandFlags.FireAndForget) {
				object result = null;
				var redisDatabase = GetRedisDatabase();
				if (async)
					redisDatabase.ScriptEvaluateAsync(script);
				else
					result = redisDatabase.ScriptEvaluate(script, null, null, flags);
				return result;
			}

			public void Slide(bool async = false)
			{
				var prefix = GetSessionItemKey(string.Empty);
				var expire = UnlockedExtensions.GetNextTimeout(configuration.SessionTimeout);
				SlideInternal(prefix, expire, async);
			}

			protected void SlideInternal(string prefix, TimeSpan expireTime, bool async = false)
			{
				if (!prefix.StartsWith(UnlockedExtensions.UNLOCKED + ":")) return;
				// local k=0;local karr=redis.call('KEYS', 'bar*'); for i, name in ipairs(karr) do redis.call('EXPIRE', name, 86400); k=i; end return k;
				string script = string.Format("local k=0;local karr=redis.call('KEYS', '{0}*'); for i, name in ipairs(karr) do redis.call('EXPIRE', name, {1}); k=i; end return k;)", prefix, Math.Ceiling(expireTime.TotalSeconds));
				Eval(script, async);
			}

			public void DeleteStartsWith(string prefix, bool async = false)
			{
				if (!prefix.StartsWith(UnlockedExtensions.UNLOCKED + ":")) return;
				// local k=0;local karr=redis.call('KEYS', '*'); for i, name in ipairs(karr) do redis.call('del', name); k=i; end return k;
				string script = string.Format("local karr=redis.call('KEYS', '{0}*'); for i, name in ipairs(karr) do redis.call('del', name); end", prefix);
				Eval(script, async);
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

			//public void Dispose()
			//{
			//	//_redisConnection.Dispose();
			//	//_disposed = true;
			//}

			//private bool _disposed = false;
			//public bool Disposed()
			//{
			//	return _disposed;
			//}

		}
}
