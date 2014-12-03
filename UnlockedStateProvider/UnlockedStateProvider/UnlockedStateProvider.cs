using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UnlockedStateProvider
{
	public class UnlockedStateProvider
	{
		public const string UNLOCKED_STATE_OBJECT_KEY = "UNLOCKED_STATE_OBJECT_KEY";
		public const string UNLOCKED_STATE_CONNECTION_KEY = "UNLOCKED_STATE_CONNECTION_KEY";

		public const string DEFAULT_COOKIE_NAME = "ASP.NET_SessionId";

		public UnlockedStateProvider(HttpContextBase controllerContext)
		{
			
		}

	}
}
