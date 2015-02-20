using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tercuman.Website
{
	/// <summary>
	/// ThemedRazorViewEngine : <a href="http://onuralptaner.com/arsivler/2013/07/18/mvc-razor-tema-destegi/" remarks="http://onuralptaner.com/arsivler/2013/07/18/mvc-razor-tema-destegi/">ThemedRazorViewEngine</a>
	/// </summary>
	public class ThemedRazorViewEngine : RazorViewEngine
	{
		private readonly Theme _theme;

		public ThemedRazorViewEngine(Theme theme)
		{
			_theme = theme;

			base.ViewLocationFormats = new[]
			{
			_theme.Path + "/Views/{1}/{0}.cshtml",
			_theme.Path + "/Views/Shared/{0}.cshtml",
			"~/Themes/Default/Views/{1}/{0}.cshtml"
			};

			base.PartialViewLocationFormats = new[] {
			_theme.Path + "/Views/{1}/{0}.cshtml",
			_theme.Path + "/Views/Shared/{0}.cshtml",
			"~/Themes/Default/Views/Shared/{0}.cshtml"
			};

			base.AreaViewLocationFormats = new[]
			{
			_theme.Path + "/Views/{2}/{1}/{0}.cshtml",
			_theme.Path + "/Views/Shared/{0}.cshtml",
			"~/Themes/Default/Views/{1}/{0}.cshtml"
			};
			base.AreaPartialViewLocationFormats = new[]
			{
			_theme.Path + "/Views/{2}/{1}/{0}.cshtml",
			_theme.Path + "/Views/{1}/{0}.cshtml",
			_theme.Path + "/Views/Shared/{0}.cshtml",
			"~/Themes/Default/Views/Shared/{0}.cshtml"
			};
		}

		public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName,
		string masterName, bool useCache)
		{
			// eğer çalışma anında temayı değiştirmek istiyorsanız viewCache'in değeri false olmaalıdır.
			const bool useViewCache = false;

			return base.FindView(controllerContext, viewName, masterName, useViewCache);
		}
	}
}