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
    public class CompanyMeetingRepository : EntityFrameworkRepository<CompanyMeeting, int>, ICompanyMeetingRepository
    {
        private readonly IngressContext _context;

        public CompanyMeetingRepository(IngressContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<CompanyMeeting>> Find(DateTime start, DateTime end)
        {
            return await _context
                .Activity
                .OfType<CompanyMeeting>()
                .Where(x => x.DateStart >= start && x.DateEnd <= end)
                .ToListAsync();
        }

        public bool Exists(string calendarId)
        {
            return _context.Activity
                .OfType<CompanyMeeting>()
                .Any(x => x.CalID == calendarId);
        }
    }
}