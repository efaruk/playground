using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnlockedStateProvider;
using UnlockedStateProvider.Redis;

namespace UnlockedSessionDemo.Controllers
{

	[RedisUnlockedStateUsage(Order = 0, Usage = UnlockedStateUsage.ReadWrite, RunAsync = true)]
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
			items["big"] = new BigSessionObject();
			ViewBag.UnlockedItems = items;
			return View();
		}
	}
}