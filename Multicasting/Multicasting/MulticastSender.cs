using System;
using System.Net;
using System.Net.Sockets;

namespace Multicasting
{
    public class MulticastSender: IDisposable
    {
        private static readonly IPAddress multicastAddress = IPAddress.Parse(GlobalConstants.MULTICAST_ADDRESS);

        private static IPEndPoint remoteEndpoint;

        private static UdpClient _udpClient;

        public MulticastSender(int multicastPortNumber)
        {
            remoteEndpoint = new IPEndPoint(multicastAddress, multicastPortNumber);
            SetupUdpClient();
        }

        private void SetupUdpClient()
        {
            _udpClient = new UdpClient();
            _udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            _udpClient.Client.ExclusiveAddressUse = false;
            //_udpClient.Client.Bind(remoteEndpoint);
            _udpClient.JoinMulticastGroup(multicastAddress, 2);
        }

        public void SendMessage(string message)
        {

            message = string.Format("{0}{2}{1}", GlobalConstants.MESSAGE_START, GlobalConstants.MESSAGE_END, message);
            var bytes = GlobalConstants.DefaultEncoding.GetBytes(message);

            _udpClient.BeginSend(bytes, bytes.Length, remoteEndpoint, SendCallback, null);
        }

        private void SendCallback(IAsyncResult ar)
        {
            var sendBytes = _udpClient.EndSend(ar);
            //_udpClient.Close();
        }

        public void Dispose()
        {
            if (_udpClient != null)
            {
                _udpClient.Close();
            }
        }
    }
}
