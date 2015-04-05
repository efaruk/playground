using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tercuman.Website
{
	/// <summary>
	/// ThemedRazorViewEngine : <a href="http://onuralptaner.com/arsivler/2013/07/18/mvc-razor-tema-destegi/" remarks="http://onuralptaner.com/arsivler/2013/07/18/mvc-razor-tema-destegi/">ThemedRazorViewEngine</a>
	/// </summary>
	public class CustomRazorViewEngine : RazorViewEngine
	{
		private readonly Theme _theme;


		public CustomRazorViewEngine(Theme theme)
		{
			_theme = theme;

			base.ViewLocationFormats = new[]
			{
			_theme.Path + "/Views/{1}/{0}.cshtml",
			_theme.Path + "/Views/" + _theme.LanguageCode + "/{1}/{0}.cshtml",
			_theme.Path + "/Views/Shared/{0}.cshtml",
			_theme.Path + "/Views/Shared/" + _theme.LanguageCode + "/{0}.cshtml",
			"~/Themes/Default/Views/{1}/{0}.cshtml",
			"~/Themes/Default/Views/" + _theme.LanguageCode + "{1}/{0}.cshtml"
			};

			base.PartialViewLocationFormats = new[] {
			_theme.Path + "/Views/{1}/{0}.cshtml",
			_theme.Path + "/Views/" + _theme.LanguageCode + "/{1}/{0}.cshtml",
			_theme.Path + "/Views/Shared/{0}.cshtml",
			_theme.Path + "/Views/Shared/" + _theme.LanguageCode + "/{0}.cshtml",
			"~/Themes/Default/Views/Shared/{0}.cshtml",
			"~/Themes/Default/Views/" + _theme.LanguageCode + "{1}/{0}.cshtml"
			};

			base.AreaViewLocationFormats = new[]
			{
			_theme.Path + "/Views/{2}/{1}/{0}.cshtml",
			_theme.Path + "/Views/" + _theme.LanguageCode + "/{2}/{1}/{0}.cshtml",
			_theme.Path + "/Views/Shared/{0}.cshtml",
			_theme.Path + "/Views/Shared/" + _theme.LanguageCode + "/{0}.cshtml",
			"~/Themes/Default/Views/{1}/{0}.cshtml",
			"~/Themes/Default/Views/" + _theme.LanguageCode + "{1}/{0}.cshtml"
			};

			base.AreaPartialViewLocationFormats = new[]
			{
			_theme.Path + "/Views/{2}/{1}/{0}.cshtml",
			_theme.Path + "/Views/" + _theme.LanguageCode + "/{2}/{1}/{0}.cshtml",
			_theme.Path + "/Views/{1}/{0}.cshtml",
			_theme.Path + "/Views/" + _theme.LanguageCode + "/{1}/{0}.cshtml",
			_theme.Path + "/Views/Shared/{0}.cshtml",
			_theme.Path + "/Views/Shared/" + _theme.LanguageCode + "/{0}.cshtml",
			"~/Themes/Default/Views/Shared/{0}.cshtml",
			"~/Themes/Default/Views/" + _theme.LanguageCode + "{1}/{0}.cshtml"
			};
		}

		public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
		{
			const bool useViewCache = false;

			return base.FindView(controllerContext, viewName, masterName, useViewCache);
		}
	}
}