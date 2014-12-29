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

	[RedisUnlockedStateUsage(Usage = UnlockedStateUsage.ReadWrite)]
	public class LogoutController : BaseController
	{
		// GET: Home
		public ActionResult Index()
		{
			SessionWrapper.Abondon();
			return View();
		}
	}
}