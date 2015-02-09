using System.Net;
using System.Net.Sockets;

namespace Multicasting
{
	public class MulticastSender
	{
		private IPAddress _ipAddress = IPAddress.Parse("224.0.0.1");

		void Test()
		{
			var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			var client = new UdpClient(AddressFamily.InterNetwork);
			client.JoinMulticastGroup(_ipAddress, 1);
			
		}
	}
}
