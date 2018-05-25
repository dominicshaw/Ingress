using System.Runtime.Serialization;

namespace Ingress.DTOs
{
    [DataContract(Name = "CM")]
    public class CompanyMeetingDTO : ActivityDTO
    {
        [DataMember(Name = "gid")]
        public string GlobalID { get; set; }
        [DataMember(Name = "cvid")]
        public string ConvoID { get; set; }
        [DataMember(Name = "org")]
        public string Organiser { get; set; }
        [DataMember(Name = "cat")]
        public string Categories { get; set; }
        [DataMember(Name = "idr")]
        public bool? IsDirect { get; set; }
        [DataMember(Name = "cid")]
        public string CalID { get; set; }

        [DataMember(Name = "skp")]
        public bool Skipped { get; set; }
    }
}