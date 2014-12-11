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
			//UnlockedExtensions.StartSessionIfNew(); // Remove Custom cookie name from app settings and use default ASP.Net session cookie
			UnlockedExtensions.StartSessionIfNewWithCustomCookie("UnlockedCookie"); // set this name to Unlocked:CookieName app setting and use custom cookie
			
			return View();
		}
	}
}