using System.Runtime.Serialization;
using LiteDB;

namespace Ingress.DTOs
{
    [DataContract(Name = "B")]
    public class BrokerDTO
    {
        [BsonId(false)]
        [DataMember(Name = "bid")]
        public int BrokerID { get; set; }
        [DataMember(Name = "n")]
        public string Name { get; set; }
    }
}