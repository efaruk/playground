using System.Web.Mvc;
using System.Web.SessionState;
using UnlockedStateProvider;
using UnlockedStateProvider.Redis;

namespace UnlockedSessionDemo.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    [RedisUnlockedStateUsage(Order = 0, Usage = UnlockedStateUsage.ReadOnly)]
    public class UnlockedBigReadonlyController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            var store = this.GetStoreFromContext();
            ViewBag.UnlockedStore = store;
            return View();
        }
    }
}