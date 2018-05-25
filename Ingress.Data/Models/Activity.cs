using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ingress.Data.Models
{
    [Table("Activity")]
    public abstract class Activity
    {
        [Key]
        public int ActivityID { get; set; }
        [Required]
        public DateTime InsertedAt { get; set; }
        [MaxLength(50)]
        [Required]
        public string Username { get; set; }
        [MaxLength(512)]
        [Required]
        public string Subject { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public decimal? BrokerId { get; set; }
        [MaxLength(200)]
        public string BrokerName { get; set; }
        public int? Rating { get; set; }
        [MaxLength(1024)]
        public string Comments { get; set; }
        [MaxLength(4)]
        public string PushOrPull { get; set; }
        [MaxLength(200)]
        public string RefID { get; set; }

        protected Activity()
        {
            InsertedAt = DateTime.Now;
            Username = Environment.UserName;
            DateStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
            DateEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 30, 0);
        }
    }
}
