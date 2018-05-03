using System.Collections.Generic;
using System.Threading.Tasks;
using Ingress.Data.Interfaces;
using Ingress.Data.Models;
using JetBrains.Annotations;

namespace Ingress.Data.Mocks
{
    [UsedImplicitly]
    public class MockActivityRepository : IActivityRepository
    {
        public async Task<List<Activity>> GetAll()
        {
            await Task.CompletedTask;
            return new List<Activity> {new PhoneCall(), new AnalystMeeting()};
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

        public void CancelChanges(Activity entity)
        {
            
        }

        public async Task SaveChanges()
        {
            await Task.CompletedTask;
        }
    }
}
