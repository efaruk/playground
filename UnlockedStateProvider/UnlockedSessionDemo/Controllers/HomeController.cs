using System.Web.Mvc;
using System.Web.SessionState;

namespace UnlockedSessionDemo.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //UnlockedExtensions.StartSessionIfNew(); // Remove Custom cookie name from app settings and use default ASP.Net session cookie
            //UnlockedExtensions.StartSessionIfNewWithCustomCookie(ConfigurationManager.AppSettings.Get("Unlocked:CookieName")); // set this name to Unlocked:CookieName app setting and use custom cookie
            // Check Unlocked:Auto configuration option
            return View();
        }
    }
}