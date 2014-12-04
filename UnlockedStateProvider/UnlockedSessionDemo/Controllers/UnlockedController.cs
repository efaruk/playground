using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnlockedStateProvider;
using UnlockedStateProvider.Redis;

namespace UnlockedSessionDemo.Controllers
{

	[RedisUnlockedStateUsage(Order = 0, Usage = UnlockedStateUsage.Enabled, RunAsync = true)]
	public class UnlockedController : Controller
	{
		// GET: Home
		public ActionResult Index()
		{
			var items = this.GetContextItems();
			items["test"] = "test";
			ViewBag.UnlockedItems = items;
			return View();
		}
	}
}