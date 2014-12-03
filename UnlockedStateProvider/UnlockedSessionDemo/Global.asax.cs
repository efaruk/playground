using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json.Serialization;
using StackExchange.Profiling;

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
		}

		//protected void Application_Error(object sender, EventArgs e)
		//{
		//	// var error = Server.GetLastError();
		//}

		protected void Application_BeginRequest()
		{
			MiniProfiler.Start();
		}

		protected void Application_EndRequest()
		{
			MiniProfiler.Stop();
		}

	}
}
