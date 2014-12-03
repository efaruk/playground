using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Web.Configuration;
using System.Web.Hosting;

namespace UnlockedStateProvider
{
	public class StoreConfiguration
	{
		private static readonly StoreConfiguration instance = new StoreConfiguration();
		private const string DEFAULT_HOST = "localhost";
		private const int DEFAULT_PORT = 6379;
		private const int DEFAULT_SESSION_TIMEOUT = 20;
		private const string DEFAULT_DATABASE_ID = "3";

		private StoreConfiguration()
		{
			Initialize();
		}

		public static StoreConfiguration Instance
		{
			get { return instance; }
		}

		private void Initialize()
		{
			Host = SettingsHelper.GetAppSetting("Unlocked:Host", DEFAULT_HOST);
			Port = SettingsHelper.GetIntAppSetting("Unlocked:Port", DEFAULT_PORT);
			SessionTimeout = SettingsHelper.GetIntAppSetting("Unlocked:Timeout", DEFAULT_SESSION_TIMEOUT);
			Database = SettingsHelper.GetAppSetting("Unlocked:Database", DEFAULT_DATABASE_ID);
		}

		/// <summary>
		/// As seconds
		/// </summary>
		public int RequestTimeout { get; set; }
		
		/// <summary>
		/// As minutes
		/// </summary>
		public int SessionTimeout { get; set; }

		/// <summary>
		/// Port number for remote store.
		/// </summary>
		public int Port { get; set; }

		/// <summary>
		/// Hostname or ip address for remote store.
		/// StackExchange.Redis compatible configuration string...
		/// </summary>
		public string Host { get; set; }

		public string AccessKey { get; set; }

		public int RetryTimeout { get; set; }

		public bool ThrowOnError { get; set; }

		public bool UseSsl { get; set; }

		/// <summary>
		/// Database identifier name or id
		/// </summary>
		public string Database { get; set; }

		public string ApplicationName { get; set; }

		public int ConnectionTimeoutInMilliSec { get; set; }

		public int OperationTimeoutInMilliSec { get; set; }

	}
}
