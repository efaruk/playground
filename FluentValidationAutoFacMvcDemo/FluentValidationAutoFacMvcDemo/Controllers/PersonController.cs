using System.Web.Mvc;
using FluentValidationAutoFacMvcDemo.Models;

namespace FluentValidationAutoFacMvcDemo.Controllers
{
    public class PersonController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(PersonModel model)
        {
            if (ModelState.IsValid)
            {
                
            }
            return View(model);
        }
    }
}