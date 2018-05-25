using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ingress.DTOs;

namespace Ingress.Mobile.Services
{
    public interface IApi
    {
        Task<bool> CheckLogin(string username, string password, CancellationToken token);
        Task<bool> SaveActivity(ActivityDTO item, CancellationToken token);
        Task<bool> DeleteActivity(int id);
        Task<ActivityDTO> GetActivity(int id);
        Task<IEnumerable<ActivityDTO>> GetActivitiesForUser(CancellationToken token, bool forceRefresh = false);
        Task<IEnumerable<BrokerDTO>> GetBrokers();
        Task<IEnumerable<string>> GetAnalysts();
        Task<IEnumerable<string>> GetUsers();
    }
}
