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
    public class AnalystMeetingRepository : EntityFrameworkRepository<AnalystMeeting, string>, IAnalystMeetingRepository
    {
        private readonly IngressContext _context;

        public AnalystMeetingRepository(IngressContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AnalystMeeting>> FindSkipped()
        {
            return await _context.Activity.OfType<AnalystMeeting>().Where(x => x.BrokerId == 419).ToListAsync();
        }
    }
}