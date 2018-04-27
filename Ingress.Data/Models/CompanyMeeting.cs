namespace Ingress.Data.Models
{
    public class CompanyMeeting : Activity
    {
        public string GlobalID { get; set; }
        public string ConvoID { get; set; }
        public string Organiser { get; set; }
        public string Categories { get; set; }
        public bool? IsDirect { get; set; }
        public string CalID { get; set; }
    }
}
