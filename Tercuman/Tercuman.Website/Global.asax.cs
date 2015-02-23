using System.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Tercuman.Website
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			
			RegisterViewEngine(ViewEngines.Engines);
		}

		public static void RegisterViewEngine(ViewEngineCollection viewEngines)
		{
			// mevcut engineleri temizliyoruz.
			viewEngines.Clear();

			var basePath = ConfigurationManager.AppSettings["ThemeBasePath"];
			var themeName = ConfigurationManager.AppSettings["ThemeName"];

			var theme = new Theme(basePath, themeName, "TR", false);

			var themeableRazorViewEngine = new CustomRazorViewEngine(theme);

			viewEngines.Add(themeableRazorViewEngine);
		}

	}
}