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

		public ConnectionMultiplexer RedisConnection
		{
			get { return _redisConnection; }
		}

		public bool Connected { get; private set; }

		public void Connect(string host)
		{
			if (Connected)
			{
				Disconnect();
			}
			var options = new ConfigurationOptions
			{
				AllowAdmin = true,
				AbortOnConnectFail = false,
				ResolveDns = true,
				ClientName = "Sifu Redis Client"
			};
			options.EndPoints.Add(host);
			_redisConnection = ConnectionMultiplexer.Connect(options);
			Connected = true;
		}

		public void Disconnect()
		{
			if (_redisConnection != null) _redisConnection.Dispose();
			_redisConnection = null;
			Connected = false;
		}

	}
}
