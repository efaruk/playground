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
	[RedisUnlockedStateUsage(Order = 0, Usage = UnlockedStateUsage.ReadWrite)]
	public class UnlockedBigController : Controller
	{
		// GET: Home
		public ActionResult Index()
		{
			var store = this.GetStoreFromContext();
			store["test"] = "test";
			store["double"] = 1123123.123m;
			store["date"] = DateTime.Now;
			store["custom"] = new CustomSessionObject() { Name = "Tamer" };
			store["big"] = new BigSessionObject();
			ViewBag.UnlockedStore = store;
			return View();
		}
	}
}