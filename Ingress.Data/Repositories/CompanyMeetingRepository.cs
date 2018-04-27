using Ingress.Data.Bases;
using Ingress.Data.Interfaces;
using Ingress.Data.Models;
using JetBrains.Annotations;

namespace Ingress.Data.Repositories
{
    [UsedImplicitly]
    public class CompanyMeetingRepository : EntityFrameworkRepository<CompanyMeeting, string>, ICompanyMeetingRepository
    {
        private readonly IngressContext _context;

        public CompanyMeetingRepository(IngressContext context) : base(context)
        {
            _context = context;
        }
    }
}