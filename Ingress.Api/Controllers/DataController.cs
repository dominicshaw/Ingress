using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Ingress.Data.Repositories;
using Ingress.DTOs;

namespace Ingress.Api.Controllers
{
    [RoutePrefix("Data")]
    public class DataController : ApiController
    {
        [HttpGet]
        [Route("Brokers")]
        public async Task<List<BrokerDTO>> Brokers()
        {
            var context = new DataSourcesRepository();

            var results = await context.GetBrokers(false);

            return results.Select(x => new BrokerDTO() {BrokerID = x.ID, Name = x.Name.ToUpper()}).ToList();
        }

        [HttpGet]
        [Route("Analysts")]
        public async Task<List<string>> Analysts()
        {
            var context = new DataSourcesRepository();

            var results = await context.GetAnalysts();

            return results.ToList();
        }
    }
}
