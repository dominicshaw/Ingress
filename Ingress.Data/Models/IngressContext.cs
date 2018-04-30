using System.Data.Entity;
using JetBrains.Annotations;

namespace Ingress.Data.Models
{
    [UsedImplicitly]
    public class IngressContext : DbContext
    {
        public DbSet<Activity> Activity { get; set; }

        public IngressContext()
            : base("Server=LONHSQL01\\PROD01;Database=Ingress;Trusted_Connection=True;")
        {
            
        }
    }
}
