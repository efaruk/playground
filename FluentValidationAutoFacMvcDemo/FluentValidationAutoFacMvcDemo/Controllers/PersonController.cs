using System.Web.Mvc;
using FluentValidAutoFacMvcDemo.Models;

namespace FluentValidAutoFacMvcDemo.Controllers
{
    public class PersonController : Controller
    {
        public ActionResult Index()
        {
            var model = new PersonModel
            {
                FirstName = "Faruk",
                LastName = "Pehlivanlı",
            };
            return View(model);
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