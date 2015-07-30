using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sims
{
    public class SimsHealthCheckHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Write("merhaba dünya");
        }

        public bool IsReusable { get { return false; } }
    }
}
