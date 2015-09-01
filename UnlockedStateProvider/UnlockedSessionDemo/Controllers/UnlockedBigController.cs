using System;
using System.Web.Mvc;
using System.Web.SessionState;
using UnlockedStateProvider;
using UnlockedStateProvider.Redis;

namespace UnlockedSessionDemo.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    [RedisUnlockedStateUsage(Order = 0, Usage = UnlockedStateUsage.ReadWrite)]
    public class UnlockedBigController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            SessionWrapper.Test = "test";
            SessionWrapper.Double = 1123123.123;
            SessionWrapper.Date = DateTime.Now;
            SessionWrapper.CustomSessionObject = new CustomSessionObject() {Name = "Tamer"};
            SessionWrapper.BigSessionObject = new BigSessionObject();
            return View();
        }
    }
}