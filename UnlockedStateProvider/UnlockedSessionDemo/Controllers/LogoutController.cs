using System.Web.Mvc;
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