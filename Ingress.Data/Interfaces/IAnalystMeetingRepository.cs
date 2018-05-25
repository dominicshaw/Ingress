using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ingress.Data.Models;

namespace Ingress.Data.Interfaces
{
    public interface IAnalystMeetingRepository : IEntityRepository<AnalystMeeting, int>
    {
        Task<List<AnalystMeeting>> FindSkipped();
        Task<List<AnalystMeeting>> Find(int? brokerId, DateTime start, DateTime end);

        bool Exists(string calendarId);
    }
}