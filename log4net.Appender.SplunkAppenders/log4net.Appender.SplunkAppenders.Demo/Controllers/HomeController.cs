using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace log4net.Appender.SplunkAppenders.Demo.Controllers
{
    public class HomeController : Controller
    {
        ILog _logger = LoggerContainer.Logger;

        public ActionResult Index()
        {
            //Parallel.For(0, 1000, i =>
            //{
            //    _logger.Info(string.Format("Test Info Log {0}", i));
            //});

            //_logger.Info(string.Format("Test Info Log {0}", "single"));
            _logger.Error("Error: Home/Index", new InvalidOperationException("Index action is not allowed for anonymous users..."));

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}