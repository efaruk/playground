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
	[RedisUnlockedStateUsage(Usage = UnlockedStateUsage.ReadWrite)]
	public class UnlockedController : Controller
	{
		// GET: Home
		public ActionResult Index()
		{
			SessionWrapper.Test = "test";
			SessionWrapper.Double = 1123123.123;
			SessionWrapper.Date = DateTime.Now;
			SessionWrapper.CustomSessionObject = new CustomSessionObject() { Name = "Tamer" };
			return View();
		}
	}
}