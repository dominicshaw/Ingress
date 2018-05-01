using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ingress.Data.Models
{
    [Table("PhoneCall")]
    public class PhoneCall : Activity
    {
        [MaxLength(128)]
        public string TimeTaken { get; set; }
        [MaxLength(512)]
        public string Analyst { get; set; }

        public PhoneCall()
        {
            TimeTaken = new TimeSpan(0, 30, 0).ToString();
        }
    }
}