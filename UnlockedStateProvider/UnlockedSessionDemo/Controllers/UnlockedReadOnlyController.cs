using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using UnlockedStateProvider;
using UnlockedStateProvider.Redis;

namespace UnlockedSessionDemo.Controllers
{
	[SessionState(SessionStateBehavior.Disabled)]
	[RedisUnlockedStateUsage(Order = 0, Usage = UnlockedStateUsage.ReadOnly)]
	public class UnlockedReadOnlyController : Controller
	{
		// GET: Home
		public ActionResult Index()
		{
			return View();
		}
	}
}