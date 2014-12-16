using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Sifu
{
	public class SharedConnection
	{
		private static readonly Lazy<SharedConnection> instance = new Lazy<SharedConnection>(() => new SharedConnection());
		private static ConnectionMultiplexer _redisConnection;

		private SharedConnection()
		{
			
		}

		public static SharedConnection Instance
		{
			get { return instance.Value; }
		}

		public void Connect(string host)
		{
			
		}
	}
}
