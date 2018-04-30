using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ingress.Data.Models
{
    [Table("AnalystMeetings")]
    public class AnalystMeeting : Activity
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

        public AnalystMeeting()
        {
            TimeTaken = new TimeSpan(0, 30, 0).ToString();
            DateStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
            DateEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 30, 0);
        }
    }
}
