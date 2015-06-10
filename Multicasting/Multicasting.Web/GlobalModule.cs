using System;

namespace Multicasting.Web
{
	public sealed class GlobalModule: IDisposable
	{
	    public const int MulticastPortNumber = 3000;
        // ReSharper disable once InconsistentNaming
		private static readonly Lazy<GlobalModule> instance = new Lazy<GlobalModule>(() => new GlobalModule(), true);
        private static readonly MulticastReceiver Receiver = new MulticastReceiver(MulticastPortNumber);

		private GlobalModule()
		{
			InitializeGlobalModule();
		}

		private void InitializeGlobalModule()
		{
            Receiver.MessageReceived += Receiver_MessageReceived;
            Receiver.Start();
		}

        private void Receiver_MessageReceived(string message)
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

	    public void Dispose()
	    {
	        Receiver.Stop();
	    }
	}
}