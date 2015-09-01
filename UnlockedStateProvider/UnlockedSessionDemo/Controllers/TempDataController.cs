using System.Web.Mvc;
using System.Web.SessionState;
using UnlockedStateProvider;
using UnlockedStateProvider.Redis;

namespace UnlockedSessionDemo.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    [RedisUnlockedStateUsage(Usage = UnlockedStateUsage.ReadOnly)]
    public class TempDataController : BaseController
    {
        public ActionResult Index()
        {
            TempData["test"] = "Temp data test";
            return RedirectToAction("Show");
        }

        public ActionResult Show()
        {
            return View();
        }
    }
}