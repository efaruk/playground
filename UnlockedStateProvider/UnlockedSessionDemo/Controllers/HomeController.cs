using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnlockedStateProvider;

namespace UnlockedSessionDemo.Controllers
{
	public class HomeController : Controller
	{
		// GET: Home
		public ActionResult Index()
		{
			//this.StartSessionIfNew();
			BigBytez.Bytez.Length.ToString();
			return View();
		}
	}
}