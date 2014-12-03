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
			
			public RedisUnlockedStateStore()
			{
				StoreConfiguration configuration = StoreConfiguration.Instance;
				_redisConnection = ConnectionMultiplexer.Connect(configuration.Host);
				_redisDatabase = _redisConnection.GetDatabase(int.Parse(configuration.Database));
			}

			//public IUnlockedStateStore UnlockedStore()
			//{
			//	return new RedisUnlockedStateStore();
			//}

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
							_items = GetContextItems(HttpContext.Current);
						}
						// if (_items == null) _items = new Dictionary<string, object>(UnlockedStateUsageAttribute.DEFAULT_ITEM_COUNT);
					}
					return _items;
				}
				set { _items = value; }
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

			public Dictionary<string, object> GetContextItems(HttpContextBase controllerContext)
			{
				var items = (Dictionary<string, object>) controllerContext.GetContextItem(UnlockedStateUsageAttribute.UNLOCKED_STATE_OBJECT_KEY);
				return items;
			}

			public Dictionary<string, object> GetContextItems(HttpContext controllerContext)
			{
				var items = (Dictionary<string, object>)controllerContext.GetContextItem(UnlockedStateUsageAttribute.UNLOCKED_STATE_OBJECT_KEY);
				return items;
			}

			public void SetContextItems(HttpContextBase controllerContext, Dictionary<string, object> items)
			{
				controllerContext.SetContextItem(UnlockedStateUsageAttribute.UNLOCKED_STATE_OBJECT_KEY, items);
			}

			public void SetContextItems(HttpContext controllerContext, Dictionary<string, object> items)
			{
				controllerContext.SetContextItem(UnlockedStateUsageAttribute.UNLOCKED_STATE_OBJECT_KEY, items);
			}

			public object Get(string key, bool slide = true, bool slideAsync = true)
			{
				var result = _redisDatabase.StringGet(key);
				if (slide)
				{
				}
				return result;
			}

			public bool Set(string key, object value, TimeSpan expireTime, bool async = false)
			{
				bool result = false;
				if(async)
					_redisDatabase.StringSetAsync(key, (RedisValue)value, expireTime);
				else
					result = _redisDatabase.StringSet(key, (RedisValue)value, expireTime);
				return result;
			}

			public bool Delete(string key, bool async = false) {
				bool result = false; 
				if (async)
					_redisDatabase.KeyDeleteAsync(key);
				else
					result = _redisDatabase.KeyDelete(key);
				
				return result;
			}

			public object Eval(string script, bool async = false) {
				object result = null; 
				if (async)
					_redisDatabase.ScriptEvaluateAsync(script);
				else
					result = _redisDatabase.ScriptEvaluate(script);
				return result;
			}
		
			public void Slide(string prefix, TimeSpan expireTime, bool async = false)
			{
				string script = string.Format("\"return redis.call('expire', unpack(redis.call('keys', ARGV[1])), ARGV[2])\" 2 {0}:* {1}", prefix, expireTime);
				Eval(script, async);
			}

			public void DeleteStartsWith(string prefix, bool async = false)
			{
				string script = string.Format("\"return redis.call('del', unpack(redis.call('keys', ARGV[1])))\" 1 {0}:*", prefix);
				Eval(script, async);
			}

			public void Dispose()
			{
				_redisConnection.Dispose();
			}

		}
}
