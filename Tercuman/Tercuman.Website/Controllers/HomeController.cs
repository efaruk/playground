using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using RestSharp;
using Tercuman.Website.Models;

namespace Tercuman.Website.Controllers
{
	
	public class HomeController : Controller
	{
		// GET: Home
		public ActionResult Index()
		{
			var rootUrl = VirtualPathUtility.ToAbsolute("~/");

			var client = new RestClient(rootUrl);
			var request = new RestRequest("api/tercuman", Method.POST)
			{
				RequestFormat = DataFormat.Json
			};

			var contentRequest = new ContentRequest()
			{
				LangCode = "tr-TR",
				FieldName = "ProductName",
				TableName = "Product",
				PrimaryKey = "1"
			};

			var response = client.Execute<ContentResponse>(request);

			if (response.StatusCode != HttpStatusCode.OK) throw response.ErrorException;

			return View();
		}
	}
}