using System;
using System.Net;
using System.Net.Sockets;

namespace Multicasting
{
    public delegate void MessageReceivedDelegate(string message);

    public class MulticastReceiver: IDisposable
    {
        private readonly int _multicastPortNumber;
        private static readonly IPAddress multicastAddress = IPAddress.Parse(GlobalConstants.MULTICAST_ADDRESS);
        private static IPEndPoint localEndpoint;

        private static bool _break;
        private static UdpClient _udpClient;

        public event MessageReceivedDelegate MessageReceived;

        public MulticastReceiver(int multicastPortNumber)
        {
            _multicastPortNumber = multicastPortNumber;
            localEndpoint = new IPEndPoint(IPAddress.Any, multicastPortNumber);
        }

        private void OnMessageReceived(string message)
        {
            if (_break) return;
            // Filter if required to decide fire MessageReceived event or not...
            if (MessageReceived != null) MessageReceived(message);
        }

        public void Start()
        {
            _break = false;
            StartListening();
        }

        public void Stop()
        {
            _break = true;
            if (_udpClient == null) return;
            _udpClient.Close();
            _udpClient = null;
        }

        private void SetupUdpClient()
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

        private void StartListening()
        {

            SetupUdpClient();
            _udpClient.BeginReceive(ReceiveCallback, null);
            //while (!_break)
            //{ }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            if (_udpClient == null) return;
            var ep = new IPEndPoint(IPAddress.Any, _multicastPortNumber);
            Byte[] receiveBytes = null;
            try
            {
                receiveBytes = _udpClient.EndReceive(ar, ref ep);
            }
            catch (ObjectDisposedException) { }
            catch (NullReferenceException) { }
            string message = "";
            if (receiveBytes != null)
            {
                message = GlobalConstants.DefaultEncoding.GetString(receiveBytes);
            }
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


        public void Dispose()
        {
            if (_udpClient != null)
            {
                Stop();
            }
        }
    }
}
