using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticLinq;
using ElasticLinq.Logging;
using ElasticLinq.Retry;

namespace Goldfinch
{
    public class ElasticSearchContext : ElasticContext
    {
        public ElasticSearchContext(ElasticConnection connection, NestElasticLinqMapping mapping = null, ILog log = null, IRetryPolicy retryPolicy = null)
            : base(connection, mapping ?? new NestElasticLinqMapping(), log, retryPolicy)
        {
        }
    }
}
