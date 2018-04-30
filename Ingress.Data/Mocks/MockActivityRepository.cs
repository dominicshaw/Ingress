using System.Collections.Generic;
using System.Threading.Tasks;
using Ingress.Data.Interfaces;
using Ingress.Data.Models;

namespace Ingress.Data.Mocks
{
    public class MockActivityRepository : IActivityRepository
    {
        public async Task<List<Activity>> GetAll()
        {
            await Task.CompletedTask;
            return new List<Activity> {new PhoneCall()};
        }

        public async Task<Activity> GetById(string id)
        {
            await Task.CompletedTask;
            return new PhoneCall();
        }

        public void Create(Activity entity)
        {
            entity.ActivityID = 1;
        }

        public void Update(Activity entity)
        {

        }

        public void Delete(Activity entity)
        {

        }

        public async Task SaveChanges()
        {
            await Task.CompletedTask;
        }
    }
}
