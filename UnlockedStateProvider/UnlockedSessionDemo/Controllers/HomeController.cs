using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using UnlockedStateProvider;

namespace UnlockedSessionDemo.Controllers
{
	[SessionState(SessionStateBehavior.Required)]
	public class HomeController : Controller
	{
		// GET: Home
		public ActionResult Index()
		{
			this.StartSessionIfNew();
			BigBytez.Bytez.Length.ToString(); // Initialize static for first request;
			return View();
		}
	}
}