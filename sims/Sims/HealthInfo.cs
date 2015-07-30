using System.Runtime.Serialization;

namespace Sims
{
    [DataContract]
    public class HealthInfo
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public int HealthPercentage { get; set; }
    }
}
