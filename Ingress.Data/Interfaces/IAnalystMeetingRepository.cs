using System.Collections.Generic;
using System.Threading.Tasks;
using Ingress.Data.Models;

namespace Ingress.Data.Interfaces
{
    public interface IAnalystMeetingRepository : IEntityRepository<AnalystMeeting, string>
    {
        Task<IEnumerable<AnalystMeeting>> FindSkipped();
    }
}