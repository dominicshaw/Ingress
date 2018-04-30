using System.Collections.Generic;
using System.Threading.Tasks;
using Ingress.Data.Interfaces;
using Ingress.Data.Models;

namespace Ingress.Data.Mocks
{
    public class MockCompanyMeetingRepository : ICompanyMeetingRepository
    {
        public async Task<List<CompanyMeeting>> GetAll()
        {
            await Task.CompletedTask;
            return new List<CompanyMeeting>() {new CompanyMeeting()};
        }

        public async Task<CompanyMeeting> GetById(string id)
        {
            await Task.CompletedTask;
            return new CompanyMeeting();
        }

        public void Create(CompanyMeeting entity)
        {
            entity.ActivityID = 1;
        }

        public void Update(CompanyMeeting entity)
        {

        }

        public void Delete(CompanyMeeting entity)
        {

        }

        public async Task SaveChanges()
        {
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<CompanyMeeting>> FindSkipped()
        {
            await Task.CompletedTask;
            return new List<CompanyMeeting>();
        }
    }
}