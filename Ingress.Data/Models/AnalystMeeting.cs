using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ingress.Data.Interfaces;

namespace Ingress.Data.Models
{
    [Table("AnalystMeeting")]
    public class AnalystMeeting : Activity, IMeeting
    {
        [MaxLength(200)]
        public string GlobalID { get; set; }
        [MaxLength(200)]
        public string ConvoID { get; set; }
        [MaxLength(255)]
        public string Organiser { get; set; }
        [MaxLength(512)]
        public string Categories { get; set; }
        [MaxLength(128)]
        public string TimeTaken { get; set; }
        [MaxLength(512)]
        public string Analyst { get; set; }
        public bool? IsConference { get; set; }
        [MaxLength(200)]
        public string CalID { get; set; }

        public bool Skipped { get; set; }

        public AnalystMeeting()
        {
            TimeTaken = TimeSpan.FromHours(1).ToString();
        }
    }
}
