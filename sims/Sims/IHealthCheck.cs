using System;
using System.Collections.Generic;

namespace Sims
{
    public interface IHealthCheck
    {

        TimeSpan HeartBeat();

        List<HealthInfo> HealthCheckMedium();

        List<HealthInfo> FullHealthCheck();
    }
}
