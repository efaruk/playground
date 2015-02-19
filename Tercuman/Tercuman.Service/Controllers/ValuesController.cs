using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Tercuman.Service.Data;
using Tercuman.Service.Models;

namespace Tercuman.Service.Controllers
{
    public class ValuesController : ApiController
    {
        public GlobDBEntities Context = new GlobDBEntities();

        // GET api/values
        public IEnumerable<string> Get()
        {
            var result = new List<string>();

            var langs = Context.Content.Select(w => w.LangCode).Distinct();

            foreach (var item in langs)
            {
                result.Add(item);
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
            var content = Context.Content.FirstOrDefault(
                w =>
                    w.FieldName == request.FieldName && w.LangCode == request.LangCode &&
                    w.TableName == request.TableName);
            if (content != null)
                result.Value = content.Value;
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