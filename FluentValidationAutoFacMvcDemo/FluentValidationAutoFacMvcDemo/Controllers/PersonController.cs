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
        public ActionResult Create(PersonModel model)
        {
            if (ModelState.IsValid)
            {
                
            }
            return Json(model);
        }
    }
}