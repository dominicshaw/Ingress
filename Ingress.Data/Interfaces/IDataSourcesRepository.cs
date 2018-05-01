using System.Collections.Generic;
using System.Threading.Tasks;
using Ingress.Data.DataSources;

namespace Ingress.Data.Interfaces
{
    public interface IDataSourcesRepository
    {
        Task<List<string>> GetAnalysts();
        Task<List<Broker>> GetBrokers();
    }
}