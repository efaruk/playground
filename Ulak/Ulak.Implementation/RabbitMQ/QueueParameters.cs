using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ulak.Implementation.RabbitMQ
{
    public class QueueParameters
    {
        public int? MessageTtl { get; set; }

        public int? Expires { get; set; }

        public int? MaxLength { get; set; }

        public int? MaxLengthBytes { get; set; }

        public string DeadLetterExchange { get; set; }

        public string DeadLetterRoutingKey { get; set; }

        public int? MaxPriority { get; set; }

        public IDictionary<string, object> GetDictionary()
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>(7);
            if (MessageTtl != null)
                parameters.Add(new KeyValuePair<string, object>("x-message-ttl", MessageTtl));
            if (Expires != null)
                parameters.Add(new KeyValuePair<string, object>("x-expires", Expires));
            if (MaxLength != null)
                parameters.Add(new KeyValuePair<string, object>("x-max-length", MaxLength));
            if (MaxLengthBytes != null)
                parameters.Add(new KeyValuePair<string, object>("x-max-length-bytes", MaxLengthBytes));
            if (DeadLetterExchange != null)
                parameters.Add(new KeyValuePair<string, object>("x-dead-letter-exchange", DeadLetterExchange));
            if (DeadLetterRoutingKey != null)
                parameters.Add(new KeyValuePair<string, object>("x-dead-letter-routing-key", DeadLetterRoutingKey));
            if (MaxPriority != null)
                parameters.Add(new KeyValuePair<string, object>("x-max-priority", MaxPriority));
            return parameters;
        }
    }
}
