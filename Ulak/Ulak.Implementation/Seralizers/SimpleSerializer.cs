using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ulak.Implementation.Seralizers
{
    /// <summary>
    /// Just string to byte[] and byte[] to string converter, DO NOT use for other types
    /// </summary>
    public class SimpleSerializer: IMessageSerializer
    {
        public T Deserialie<T>(byte[] dataToDeserialize)
        {
            var json = Encoding.UTF8.GetString(dataToDeserialize);
            var result = (T)(object)json;
            return result;
        }

        public byte[] Serialize(object objecToSerialize)
        {
            var result = Encoding.UTF8.GetBytes((string)objecToSerialize);
            return result;
        }
    }
}
