using System;
using System.Runtime.Serialization;

namespace Ingress.DTOs
{
    [DataContract(Name = "AM")]
    public class AnalystMeetingDTO : ActivityDTO
    {
        [DataMember(Name = "gid")]
        public string GlobalID { get; set; }
        [DataMember(Name = "cvid")]
        public string ConvoID { get; set; }
        [DataMember(Name = "org")]
        public string Organiser { get; set; }
        [DataMember(Name = "cat")]
        public string Categories { get; set; }
        [DataMember(Name = "tt")]
        public string TimeTaken { get; set; }
        [DataMember(Name = "ana")]
        public string Analyst { get; set; }
        [DataMember(Name = "ic")]
        public bool? IsConference { get; set; }
        [DataMember(Name = "cid")]
        public string CalID { get; set; }

        [DataMember(Name = "skp")]
        public bool Skipped { get; set; }

        public AnalystMeetingDTO()
        {
            TimeTaken = new TimeSpan(0, 30, 0).ToString();
        }
    }
}