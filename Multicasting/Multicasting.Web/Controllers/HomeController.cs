using System.Web.Mvc;

namespace Multicasting.Web.Controllers {
	public class HomeController : Controller {
		public ActionResult Index() {
			ViewBag.Message = "Hoşgeldin birader...";
			Session["init"] = 1;
			foreach (string serverVariable in Request.ServerVariables) {
				Session[serverVariable] = Request[serverVariable];
			}
			return View();
		}

	}
}
