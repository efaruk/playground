using System.Web.Mvc;
using UnlockedStateProvider;
using UnlockedStateProvider.Redis;

namespace UnlockedSessionDemo.Controllers
{
	public class BaseController : Controller
	{

		protected override ITempDataProvider CreateTempDataProvider()
		{
			var provider = new InternalCookieTempDataProvider();
			return provider;
		}
	}
}