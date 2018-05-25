using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Ingress.Data.Bases;
using Ingress.Data.Interfaces;
using Ingress.Data.Models;
using JetBrains.Annotations;

namespace Ingress.Data.Repositories
{
    [UsedImplicitly]
    public class ActivityRepository : EntityFrameworkRepository<Activity, int>, IActivityRepository
    {
        private readonly IngressContext _context;

        public ActivityRepository(IngressContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Activity>> GetByUsername(string username)
        {
            return await _context.Activity.Where(x => x.Username == username).ToListAsync();
        }

        public async Task<List<string>> GetUsers()
        {
            return await _context.Activity.Select(x => x.Username).Distinct().ToListAsync();
        }
    }
}