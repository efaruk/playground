using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Ulak.Implementation.Seralizers
{
    public class JsonNetSerializer: IMessageSerializer
    {
        public T Deserialie<T>(byte[] dataToDeserialize)
        {
            var json = Encoding.UTF8.GetString(dataToDeserialize);
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }

        public byte[] Serialize(object objecToSerialize)
        {
            var json = JsonConvert.SerializeObject(objecToSerialize);
            var result = Encoding.UTF8.GetBytes(json);
            return result;
        }
    }

}
