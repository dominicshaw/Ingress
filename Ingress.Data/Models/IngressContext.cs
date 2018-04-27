using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Ingress.Data.Models
{
    [UsedImplicitly]
    public class IngressContext : DbContext
    {
        public DbSet<Activity> Activity { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=LONHSQL01\PROD01;Database=Ingress;Trusted_Connection=True;");
            }
        }
    }
}
