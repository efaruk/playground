using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Multicasting.Web
{
	public sealed class MulticastingHub : Hub
	{
		private Lazy<MulticastingHub> _instance = new Lazy<MulticastingHub>(() => new MulticastingHub());

		public MulticastingHub Instance()
		{
			return _instance.Value;
		}

		private MulticastingHub()
		{

		}

		public void Send(string name, string message)
		{
			// Call the broadcastMessage method to update clients.
			Clients.All.broadcastMessage(name, message);
		}

		public void SignalRMessageReceived(string message)
		{
			
		}
	}
}