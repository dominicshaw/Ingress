namespace Ingress.Data.Models
{
    public class AnalystMeeting : Activity
    {
        public string GlobalID { get; set; }
        public string ConvoID { get; set; }
        public string Organiser { get; set; }
        public string Categories { get; set; }
        public string TimeTaken { get; set; }
        public string Analyst { get; set; }
        public bool? IsConference { get; set; }
        public string CalID { get; set; }
    }
}
