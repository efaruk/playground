using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Multicasting
{
	public static class MulticastSender
	{
		private static readonly IPAddress multicastAddress = IPAddress.Parse(GlobalConstants.MULTICAST_ADDRESS);
		
		private static readonly IPEndPoint remoteEndpoint = new IPEndPoint(multicastAddress, GlobalConstants.MULTICAST_PORT_NUMBER);

		private static UdpClient _udpClient;

		private static void SetupUdpClient()
		{
			if (_udpClient == null)
			{
				_udpClient = new UdpClient();
				_udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
				_udpClient.Client.ExclusiveAddressUse = false;
				//_udpClient.Client.Bind(remoteEndpoint);
				_udpClient.JoinMulticastGroup(multicastAddress, 2);
			}
		}

		public static void SendMessage(string message)
		{

			SetupUdpClient();
			
			message = string.Format("{0}{2}{1}", GlobalConstants.MESSAGE_START, GlobalConstants.MESSAGE_END, message);
			var bytes = GlobalConstants.DefaultEncoding.GetBytes(message);

			_udpClient.BeginSend(bytes, bytes.Length, remoteEndpoint, SendCallback, null);
		}

		private static void SendCallback(IAsyncResult ar)
		{
			var sendBytes = _udpClient.EndSend(ar);
			//_udpClient.Close();
		}

	}
}
