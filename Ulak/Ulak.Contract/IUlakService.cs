using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ulak.Contract
{
    public interface IUlakService
    {
        void DeclareChannel(string channelName);

        void DeclareChannelQueue(string queueName);

        void BindQueue(string channelName, string queueName);
    }
}
