using System.Collections.Generic;
using System.Threading.Tasks;
using Ingress.Data.DataSources;
using Ingress.Data.Interfaces;
using JetBrains.Annotations;

namespace Ingress.Data.Mocks
{
    [UsedImplicitly]
    public class MockDataSourcesRepository : IDataSourcesRepository
    {
        public async Task<List<string>> GetAnalysts()
        {
            await Task.CompletedTask;
            return new List<string>() { "An analyst" };
            //return new List<Analyst>() { new Analyst("An analyst") };
        }

        public async Task<List<Broker>> GetBrokers()
        {
            await Task.CompletedTask;
            return new List<Broker>() { new Broker(1, "A broker") };
        }
    }
}