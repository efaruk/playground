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
			var items = this.GetContextItems();
			var store = this.GetStoreFromContext();
			store["test"] = "test";
			items["test"] = "test";
			items["double"] = 1123123.123m;
			items["date"] = DateTime.Now;
			items["custom"] = new CustomSessionObject() {Name = "Tamer"};
			items["big"] = new BigSessionObject();
			ViewBag.UnlockedItems = items;
			return View();
		}
	}
}