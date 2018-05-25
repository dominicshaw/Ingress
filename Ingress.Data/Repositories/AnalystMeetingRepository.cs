using System;
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
    public class AnalystMeetingRepository : EntityFrameworkRepository<AnalystMeeting, int>, IAnalystMeetingRepository
    {
        private readonly IngressContext _context;

        public AnalystMeetingRepository(IngressContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<AnalystMeeting>> FindSkipped()
        {
            return await _context.Activity.OfType<AnalystMeeting>().Where(x => x.BrokerId == 419).ToListAsync();
        }

        public async Task<List<AnalystMeeting>> Find(int? brokerId, DateTime start, DateTime end)
        {
            return await _context
                .Activity
                .OfType<AnalystMeeting>()
                .Where(x => x.BrokerId == 419 || !brokerId.HasValue)
                .Where(x => x.DateStart >= start && x.DateEnd <= end)
                .ToListAsync();
        }

        public bool Exists(string calendarId)
        {
            return _context.Activity
                .OfType<AnalystMeeting>()
                .Any(x => x.CalID == calendarId);
        }
    }
}