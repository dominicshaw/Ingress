using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ingress.Data.Interfaces;

namespace Ingress.Data.Models
{
    [Table("CompanyMeeting")]
    public class CompanyMeeting : Activity, IMeeting
    {
        [MaxLength(200)]
        public string GlobalID { get; set; }
        [MaxLength(200)]
        public string ConvoID { get; set; }
        [MaxLength(255)]
        public string Organiser { get; set; }
        [MaxLength(512)]
        public string Categories { get; set; }
        public bool? IsDirect { get; set; }
        [MaxLength(200)]
        public string CalID { get; set; }

        public bool Skipped { get; set; }
    }
}
