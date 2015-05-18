using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Ulak.Contract;

namespace Ulak
{
    
    public class UlakService : IUlakService
    {
        public void DeclareChannel(string channelName)
        {
            throw new NotImplementedException();
        }

        public void DeclareChannelQueue(string queueName)
        {
            throw new NotImplementedException();
        }

        public void BindQueue(string channelName, string queueName)
        {
            throw new NotImplementedException();
        }
    }
}
