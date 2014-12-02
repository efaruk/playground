using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace UnlockedStateProvider.Redis
{
		public class UnlockedRedisStore : IUnlockedStore
		{
			private readonly IDatabase _redisDatabase;

			public UnlockedRedisStore()
			{
				ConnectionMultiplexer redisConnection = ConnectionMultiplexer.Connect("localhost");
				_redisDatabase = redisConnection.GetDatabase(3);
			}

			public object Get(string key)
			{
				var result = _redisDatabase.StringGet(key);
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
				string script = string.Format("{0} {1}", prefix, expireTime);
				Eval(script, async);
			}

			public bool AutoSlidingSupport {
				get {
					return false;
				}
			}

			public bool AsyncSupport {
				get {
					return true;
				}
			}
		}
}
