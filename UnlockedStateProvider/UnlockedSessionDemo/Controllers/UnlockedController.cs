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
			var items = this.GetContextItems();
			items["test"] = "test";
			items["double"] = 1123123.123m;
			items["date"] = DateTime.Now;
			items["custom"] = new CustomSessionObject() {Name = "Tamer"};
			ViewBag.UnlockedItems = items;
			return View();
		}
	}
}