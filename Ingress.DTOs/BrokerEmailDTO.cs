using System.Runtime.Serialization;

namespace Ingress.DTOs
{
    [DataContract(Name = "BE")]
    public class BrokerEmailDTO : ActivityDTO
    {
        [DataMember(Name = "ana")]
        public string Analyst { get; set; }
    }
}