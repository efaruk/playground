using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ElasticLinq;
using ElasticLinq.Logging;
using ElasticLinq.Mapping;
using ElasticLinq.Retry;

namespace Goldfinch.Demo.DAL
{
    public class SimpleItemElasticContext : ElasticContext
    {

        public SimpleItemElasticContext()
            : base(new ElasticConnection(new Uri(DALSettingsWrapper.ElasticSearchEndpoint), index: "goldfinch"))
        {
            
        }

        public SimpleItemElasticContext(string uri, string index = null) : base(new ElasticConnection(new Uri(uri), index: string.IsNullOrWhiteSpace(index)? "goldfinch": index))
        {
            
        }
    }
}
