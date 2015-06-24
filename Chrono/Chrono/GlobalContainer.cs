using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightInject;

namespace Chrono
{
    public sealed class GlobalContainer
    {
        private static readonly GlobalContainer instance = new GlobalContainer();
        ServiceContainer serviceContainer = new ServiceContainer();

        private GlobalContainer()
        {
            
        }

        public static GlobalContainer Instance
        {
            get { return instance; }
        }


        public ServiceContainer ServiceContainer
        {
            get { return serviceContainer; }
        }


    }
}
