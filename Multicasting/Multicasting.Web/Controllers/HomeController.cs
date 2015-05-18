using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace Multicasting.Web.Controllers {
	public class HomeController : Controller {
		public ActionResult Index()
		{
			if (Request.Form.AllKeys.Contains("message"))
			{
				var message = Request.Form.Get("message");
				MulticastSender.SendMessage(message);
			}
			Thread.Sleep(100);
			var module = GlobalModule.Instance;
			ViewBag.Title = "UDP Multi-casting Demo";
			ViewBag.Message = "UDP Multicasting Send & Receive";
			ViewBag.ReceivedMessage = module.LastReceivedMessage;
			return View();
		}

	}
}
