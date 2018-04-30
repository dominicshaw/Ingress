using System.Collections.Generic;
using System.Threading.Tasks;
using Ingress.Data.Interfaces;
using Ingress.Data.Models;

namespace Ingress.Data.Mocks
{
    public class MockAnalystMeetingRepository : IAnalystMeetingRepository
    {
        public async Task<List<AnalystMeeting>> GetAll()
        {
            await Task.CompletedTask;
            return new List<AnalystMeeting>() {new AnalystMeeting()};
        }

        public async Task<AnalystMeeting> GetById(string id)
        {
            await Task.CompletedTask;
            return new AnalystMeeting();
        }

        public void Create(AnalystMeeting entity)
        {
            entity.ActivityID = 1;
        }

        public void Update(AnalystMeeting entity)
        {

        }

        public void Delete(AnalystMeeting entity)
        {

        }

        public async Task SaveChanges()
        {
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<AnalystMeeting>> FindSkipped()
        {
            await Task.CompletedTask;
            return new List<AnalystMeeting>();
        }
    }
}