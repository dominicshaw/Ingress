using System.Configuration;
using System.Data.Entity;
using JetBrains.Annotations;

namespace Ingress.Data.Models
{
    [UsedImplicitly]
    public class IngressContext : DbContext
    {
        public DbSet<Activity> Activity { get; set; }

        public IngressContext()
            : base(ConfigurationManager.ConnectionStrings["IngressDb"].ConnectionString)
        {
            
        }
    }
}
