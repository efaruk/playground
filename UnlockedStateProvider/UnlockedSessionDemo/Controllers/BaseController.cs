using System.Web.Mvc;
using UnlockedStateProvider.Redis;

namespace UnlockedSessionDemo.Controllers
{
    public class BaseController : Controller
    {
        protected override ITempDataProvider CreateTempDataProvider()
        {
            var provider = new RedisUnlockedTempDataProvider();
            return provider;
        }
    }
}