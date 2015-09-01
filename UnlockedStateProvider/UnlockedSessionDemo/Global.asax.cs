using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace UnlockedSessionDemo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Initialize static for first request;
            BigBytez.Bytez.Length.ToString();
        }

        //protected void Application_Error(object sender, EventArgs e)
        //{
        //	// var error = Server.GetLastError();
        //}

        protected void Application_BeginRequest()
        {
            //MiniProfiler.Start();
        }

        protected void Application_EndRequest()
        {
            //MiniProfiler.Stop();
        }
    }
}