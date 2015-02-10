using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Win32;
using StackExchange.Redis;

namespace Sifu
{
	public class SharedConnection
	{
		private static readonly Lazy<SharedConnection> instance = new Lazy<SharedConnection>(() => new SharedConnection());
		private static ConnectionMultiplexer _redisConnection;
		private static System.Timers.Timer timer = new System.Timers.Timer();
		private int _refreshInterval;

		public delegate void ConnectionStatusChanged(bool connected);

		public event ConnectionStatusChanged OnSharedConnectionStatusChanged;

		public event ElapsedEventHandler OnTimerElapsed;

		private SharedConnection()
		{
			timer.Elapsed += timer_Elapsed;
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

		public int RefreshInterval
		{
			get { return _refreshInterval; }
			set
			{
				if (value == 0)
				{
					timer.Stop();
				}
				else
				{
					timer.Interval = value * 1000;
					timer.Start();
				}
				_refreshInterval = value;
			}
		}

		public void Connect(string host, bool autoRefresh = false, int refreshInterval = 0)
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
			if (OnSharedConnectionStatusChanged != null)
			{
				OnSharedConnectionStatusChanged(Connected);
			}
		}

		public void Disconnect()
		{
			if (_redisConnection != null)
			{
				_redisConnection.Close();
				_redisConnection.Dispose();
			}
			_redisConnection = null;
			Connected = false;
			if (OnSharedConnectionStatusChanged != null)
			{
				OnSharedConnectionStatusChanged(Connected);
			}
		}

		void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if (OnTimerElapsed != null)
			{
				OnTimerElapsed(sender, e);
			}
		}

	}
}
