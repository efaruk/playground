using System;

namespace Multicasting.Web
{
	public sealed class GlobalModule
	{
		private static readonly Lazy<GlobalModule> instance = new Lazy<GlobalModule>(() => new GlobalModule());

		private GlobalModule()
		{
			InitializeGlobalModule();
		}

		private void InitializeGlobalModule()
		{
			
		}

		internal void MulticastReceiver_MessageReceived(string message)
		{
			ReceivedMessageCount++;
			LastReceivedMessage = message;
		}

		public static GlobalModule Instance
		{
			get { return instance.Value; }
		}

		public int SentMessageCount { get; set; }

		public int ReceivedMessageCount { get; set; }

		public string LastSentMessage { get; set; }

		public string LastReceivedMessage { get; set; }


	}
}