using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ulak.Implementation.Seralizers
{
    public interface IMessageSerializer
    {

        T Deserialie<T>(byte[] dataToDeserialize);

        byte[] Serialize(object objecToSerialize);

    }
}
