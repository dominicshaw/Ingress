using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ingress.Data.Models
{
    [Table("ModelAccess")]
    public class ModelAccess : Activity
    {
        [MaxLength(128)]
        public string TimeTaken { get; set; }
        [MaxLength(512)]
        public string Analyst { get; set; }

        public ModelAccess()
        {
            TimeTaken = new TimeSpan(0, 30, 0).ToString();
            DateStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
            DateEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 30, 0);
        }
    }
}