namespace UnlockedStateProvider
{
	public class UnlockedStateStoreConfiguration
	{
		private static readonly UnlockedStateStoreConfiguration instance = new UnlockedStateStoreConfiguration();
		
		private const string DEFAULT_HOST = "localhost";
		//private const int DEFAULT_PORT = 6379;
		private const int DEFAULT_SESSION_TIMEOUT = 20;
		private const int DEFAULT_OPERATION_TIMEOUT = 10;
		private const string DEFAULT_DATABASE_ID = "3";

		private UnlockedStateStoreConfiguration()
		{
			Initialize();
		}

		public static UnlockedStateStoreConfiguration Instance
		{
			get { return instance; }
		}

		private void Initialize()
		{
			Disabled = SettingsHelper.GetBoolAppSetting("Unlocked:Disabled", false);
			AutoManageSessionCookie = SettingsHelper.GetBoolAppSetting("Unlocked:Auto", true);
			CookieName = SettingsHelper.GetAppSetting("Unlocked:CookieName", UnlockedExtensions.CUSTOM_COOKIE_NAME);
			ForceSlide = SettingsHelper.GetBoolAppSetting("Unlocked:ForceSlide", true);
			HostConfigName = SettingsHelper.GetAppSetting("Unlocked:HostConfigName");
			Host = SettingsHelper.GetAppSetting((!string.IsNullOrWhiteSpace(HostConfigName) ? HostConfigName : "Unlocked:Host"), DEFAULT_HOST);
			//Port = SettingsHelper.GetIntAppSetting("Unlocked:Port", DEFAULT_PORT);
			SessionTimeout = SettingsHelper.GetIntAppSetting("Unlocked:SessionTimeout", DEFAULT_SESSION_TIMEOUT);
			Database = SettingsHelper.GetAppSetting("Unlocked:Database", DEFAULT_DATABASE_ID);
			OperationTimeout = SettingsHelper.GetIntAppSetting("Unlocked:OperationTimeout", DEFAULT_OPERATION_TIMEOUT);
			ConnectionTimeout = SettingsHelper.GetIntAppSetting("Unlocked:ConnectionTimeout", DEFAULT_OPERATION_TIMEOUT);
			AccessKey = SettingsHelper.GetAppSetting("Unlocked:AccessKey");
			RetryCount = SettingsHelper.GetIntAppSetting("Unlocked:RetryCount", 0);
			UseSsl = SettingsHelper.GetBoolAppSetting("Unlocked:UseSsl", false);
			ApplicationName = SettingsHelper.GetAppSetting("Unlocked:Application", UnlockedExtensions.DEFAULT_APPLICATION_NAME);
		}

		


		public bool Disabled { get; set; }

		private bool _autoManageSessionCookie = true;
		/// <summary>
		/// Auto manage session cookie and id
		/// </summary>
		public bool AutoManageSessionCookie
		{
			get { return _autoManageSessionCookie; }
			set { _autoManageSessionCookie = value; }
		}

		/// <summary>
		/// Force store to slide expiration for every hit by get to forward.
		/// </summary>
		public bool ForceSlide { get; set; }

		private string _cookieName = "";
		

		public string CookieName
		{
			get { return _cookieName; }
			set { _cookieName = value; }
		}

		/// <summary>
		/// As seconds
		/// </summary>
		public int ConnectionTimeout { get; set; }

		/// <summary>
		/// As seconds
		/// </summary>
		public int OperationTimeout { get; set; }

		/// <summary>
		/// As minutes
		/// </summary>
		public int SessionTimeout { get; set; }

		/// <summary>
		/// AppSettings key name for Host addresses...
		/// </summary>
		public string HostConfigName { get; set; }

		///// <summary>
		///// Port number for remote store.
		///// </summary>
		//public int Port { get; set; }

		/// <summary>
		/// Hostname or ip address for remote store.
		/// StackExchange.Redis compatible configuration string...
		/// </summary>
		public string Host { get; set; }


		public string AccessKey { get; set; }

		public int RetryCount { get; set; }

		///// <summary>
		///// As Seconds
		///// </summary>
		//public int RetryDelay { get; set; }

		public bool ThrowOnError { get; set; }

		public bool UseSsl { get; set; }

		/// <summary>
		/// Database identifier name or id
		/// </summary>
		public string Database { get; set; }


		public string ApplicationName { get; set; }

		public int ConnectionTimeoutInMilliSec
		{
			get { return ConnectionTimeout * 1000; }
			set { ConnectionTimeout = value / 1000; }
		}

		public int OperationTimeoutInMilliSec
		{
			get { return OperationTimeout * 1000; }
			set { OperationTimeout = value / 1000; }
		}

	}
}
