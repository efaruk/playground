using Nancy;
using WebAutoLogin.Configuration;

namespace WebAutoLogin.Service.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = p => Response.AsText("Welcome to WebAutoLogin Service");

            Get["/settings"] = p => Response.AsJson(new AutoLoginSettings());
        }
    }
}
