using System.Web.Mvc;
using System.Web.SessionState;

namespace Tercuman.Website.Controllers
{
	[SessionState(SessionStateBehavior.Disabled)]
	public class HomeController : Controller
	{
		// GET: Home
		public ActionResult Index()
		{
			return View();
		}
	}
}