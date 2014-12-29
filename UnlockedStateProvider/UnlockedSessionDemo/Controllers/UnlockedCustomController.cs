using System;
using System.Web.Mvc;
using System.Web.SessionState;
using UnlockedStateProvider;
using UnlockedStateProvider.Redis;

namespace UnlockedSessionDemo.Controllers
{

	[SessionState(SessionStateBehavior.Disabled)]
	[RedisUnlockedStateUsage(Usage = UnlockedStateUsage.CustomOnly)]
	public class UnlockedCustomController : BaseController
	{
		// GET: Home
		public ActionResult Index()
		{
			SessionWrapper.SetCustom("custom", "custom data");
			return View();
		}
	}
}