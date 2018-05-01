using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ingress.Data.Models
{
    [Table("ModelAccess")]
    public class ModelAccess : Activity
    {
        [MaxLength(512)]
        public string Analyst { get; set; }
    }
}