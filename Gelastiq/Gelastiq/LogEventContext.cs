using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticLinq;
using ElasticLinq.Logging;
using ElasticLinq.Mapping;
using ElasticLinq.Retry;

namespace Gelastiq
{
    public class LogEventContext: ElasticContext
    {
        public LogEventContext(string uri, ILog log = null, IRetryPolicy retryPolicy = null)
            : base(new ElasticConnection(new Uri(uri), index: "logark"), new LogEventMapping(), log, retryPolicy)
        {
            
        }

        //public new IQueryable<T> Query<T>()
        //    where T : BaseElasticDocument
        //{
        //    return base.Query<T>().Where(doc => doc.type == typeof(T).Name.ToCamelCase(CultureInfo.CurrentCulture)); ;
        //}
    }
}
