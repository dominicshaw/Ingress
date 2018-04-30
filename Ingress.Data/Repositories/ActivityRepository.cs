using Ingress.Data.Bases;
using Ingress.Data.Interfaces;
using Ingress.Data.Models;
using JetBrains.Annotations;

namespace Ingress.Data.Repositories
{
    [UsedImplicitly]
    public class ActivityRepository : EntityFrameworkRepository<Activity, string>, IActivityRepository
    {
        private readonly IngressContext _context;

        public ActivityRepository(IngressContext context) : base(context)
        {
            _context = context;
        }
    }
}