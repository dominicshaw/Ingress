using System;
using System.Runtime.Serialization;

namespace Ingress.DTOs
{
    [DataContract(Name = "PC")]
    public class PhoneCallDTO : ActivityDTO
    {
        [DataMember(Name = "tt")]
        public string TimeTaken { get; set; }
        [DataMember(Name = "ana")]
        public string Analyst { get; set; }

        public PhoneCallDTO()
        {
            TimeTaken = new TimeSpan(0, 30, 0).ToString();
        }
    }
}