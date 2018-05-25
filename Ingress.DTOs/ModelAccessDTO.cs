using System.Runtime.Serialization;

namespace Ingress.DTOs
{
    [DataContract(Name = "MA")]
    public class ModelAccessDTO : ActivityDTO
    {
        [DataMember(Name = "ana")]
        public string Analyst { get; set; }
    }
}