using System.Collections.Generic;
using System.Threading.Tasks;
using Ingress.Data.Models;

namespace Ingress.Data.Interfaces
{
    public interface IActivityRepository : IEntityRepository<Activity, int>
    {
        Task<List<Activity>> GetByUsername(string username);
        Task<List<string>> GetUsers();
    }
}