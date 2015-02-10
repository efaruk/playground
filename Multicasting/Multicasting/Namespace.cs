using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Multicasting
{

	public static class GlobalConstants
	{
		public const string MESSAGE_START = "|%$%|";
		public const string MESSAGE_END = "|%€|";

		public const string MULTICAST_ADDRESS = "224.0.0.1";
		public const int MULTICAST_PORT_NUMBER = 3333;

		public static Encoding DefaultEncoding = Encoding.UTF8;

	}
	
}
