using Nancy;

namespace WebAutoLogin.Service.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = p => Response.AsText("Welcome to WebAutoLogin Service");
        }
    }
}
