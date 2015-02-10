using System;
using System.Net;
using System.Net.Sockets;

namespace Multicasting
{
	public delegate void MessageReceivedDelegate(string message);

	public static class MulticastReceiver
	{
		private static readonly IPAddress multicastAddress = IPAddress.Parse(GlobalConstants.MULTICAST_ADDRESS);
		private static readonly IPEndPoint localEndpoint = new IPEndPoint(IPAddress.Any, GlobalConstants.MULTICAST_PORT_NUMBER);

		private static bool _break;
		private static UdpClient _udpClient;

		public static event MessageReceivedDelegate MessageReceived;

		private static void OnMessageReceived(string message)
		{
			if (_break) return;
			// Filter if required to decide fire MessageReceived event or not...
			if (MessageReceived != null) MessageReceived(message);
		}

		public static void Start()
		{
			_break = false;
			StartListening();
		}

		public static void Stop()
		{
			_break = true;
			_udpClient.Close();
			_udpClient = null;
		}

		private static void SetupUdpClient()
		{
			if (_udpClient == null)
			{
				_udpClient = new UdpClient();
				_udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
				_udpClient.Client.ExclusiveAddressUse = false;
				_udpClient.Client.Bind(localEndpoint);
				_udpClient.JoinMulticastGroup(multicastAddress, 2);
			}
		}

		private static void StartListening()
		{

			SetupUdpClient();
			_udpClient.BeginReceive(ReceiveCallback, null);
			while (!_break)
			{ }
		}

		private static void ReceiveCallback(IAsyncResult ar)
		{
			var ep = new IPEndPoint(IPAddress.Any, GlobalConstants.MULTICAST_PORT_NUMBER);
			Byte[] receiveBytes = _udpClient.EndReceive(ar, ref ep);

			var message = GlobalConstants.DefaultEncoding.GetString(receiveBytes);
			if (message.StartsWith(GlobalConstants.MESSAGE_START) && message.EndsWith(GlobalConstants.MESSAGE_END))
			{
				//Trim message from prepuce
				message = message.Replace(GlobalConstants.MESSAGE_START, "").Replace(GlobalConstants.MESSAGE_END, "");
				//Fire MessageReceived event with filtering
				OnMessageReceived(message);
			}
			if (!_break)
			{
				_udpClient.BeginReceive(ReceiveCallback, null);
			}
		}

		

	}
}
