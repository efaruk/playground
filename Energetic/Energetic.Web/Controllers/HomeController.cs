using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Energetic.Data.Entities;
using Energetic.DataAccess.Repository;

namespace Energetic.Web.Controllers
{
    public class HomeController : Controller
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["SqliteConnection"].ConnectionString;

        public HomeController()
        {
        }

        public ActionResult Index()
        {
            Customer customer = null;
            using (var repository = new CustomerRepository(ConnectionString))
            {
                customer = repository.Get("C0A453A2-CAED-47DB-9A87-4E57A68C247C");
            }
            return View(customer);
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