using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tercuman.Website.Data;
using Tercuman.Website.Models;

namespace Tercuman.Website.Controllers
{
	public class TercumanController : ApiController
	{

		//public GlobDBEntities DbContext = new GlobDBEntities();

		// GET api/values
		public IEnumerable<string> Get()
		{
			var result = new List<string>();

			using (var dbContext = new GlobDBEntities())
			{
				var langs = dbContext.Content.Select(w => w.LangCode).Distinct();
				result.AddRange(langs);
			}

			return result.ToList();
		}

		// GET api/values/5
		//public ContentResponse Get(ContentRequest request)
		//{
		//    return new ContentResponse
		//    {
		//        Value = "Deneme"
		//    };
		//}

		// POST api/values
		//Model {"LangCode":"en-us","TableName":"Product","FieldName":"ProductName"}
		public ContentResponse Post([FromBody]ContentRequest request)
		{
			var result = new ContentResponse();
			using (var dbContext = new GlobDBEntities())
			{
				var content = dbContext.Content.FirstOrDefault(
				w => w.LangCode == request.LangCode && w.TableName == request.TableName && w.FieldName == request.FieldName);
				if (content != null)
					result.Value = content.Value;
			}
			
			return result;
		}

		// PUT api/values/5
		//public void Put(int id, [FromBody]string value)
		//{
		//}

		//// DELETE api/values/5
		//public void Delete(int id)
		//{
		//}
	}
}