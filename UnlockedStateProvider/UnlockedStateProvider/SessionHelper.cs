using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace UnlockedStateProvider
{
	public static class SessionHelper
	{

		public static void StartSessionIfNew(this HttpContext context)
		{
			if (context != null && context.Session != null &&
				context.Session.IsNewSession && !context.Session.IsReadOnly)
			{
				context.Session[UnlockedStateUsageAttribute.SESSION_STARTED_KEY] = true;
			}
		}

		public static void StartSessionIfNew(this ControllerContext context)
		{
			if (context != null && context.HttpContext != null && context.HttpContext.Session != null &&
				context.HttpContext.Session.IsNewSession && !context.HttpContext.Session.IsReadOnly)
			{
				context.HttpContext.Session[UnlockedStateUsageAttribute.SESSION_STARTED_KEY] = true;
		
			}
		}

		public static void StartSessionIfNew(this Controller controller)
		{
			if (controller != null && controller.HttpContext != null && controller.HttpContext.Session != null &&
				controller.HttpContext.Session.IsNewSession && !controller.HttpContext.Session.IsReadOnly)
			{
				controller.HttpContext.Session[UnlockedStateUsageAttribute.SESSION_STARTED_KEY] = true;
			}
		}
		
	}
}
