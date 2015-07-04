using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goldfinch
{
    public delegate void DataAdded(object key, object data);

    public delegate void DataUpdated(object key, object data);

    public delegate void DataDeleted(object key);

}
