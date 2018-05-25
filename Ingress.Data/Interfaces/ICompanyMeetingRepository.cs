using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ingress.Data.Models;

namespace Ingress.Data.Interfaces
{
    public interface ICompanyMeetingRepository : IEntityRepository<CompanyMeeting, int>
    {
        Task<List<CompanyMeeting>> Find(DateTime start, DateTime end);

        bool Exists(string calendarId);
    }
}