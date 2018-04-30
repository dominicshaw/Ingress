using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ingress.Data.Models
{
    [Table("BrokerEmail")]
    public class BrokerEmail : Activity
    {
        [MaxLength(512)]
        public string Analyst { get; set; }
    }
}