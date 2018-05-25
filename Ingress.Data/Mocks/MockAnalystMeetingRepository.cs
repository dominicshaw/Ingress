using System;
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
            return new List<AnalystMeeting>() { new AnalystMeeting() };
        }

        public async Task<AnalystMeeting> GetById(int id)
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

        public Task Reload(AnalystMeeting entity)
        {
            return Task.CompletedTask;
        }

        public void CancelChanges(AnalystMeeting entity)
        {

        }

        public async Task SaveChanges()
        {
            await Task.CompletedTask;
        }

        public async Task<List<AnalystMeeting>> FindSkipped()
        {
            await Task.CompletedTask;
            return new List<AnalystMeeting>();
        }

        public async Task<List<AnalystMeeting>> Find(int? brokerId, DateTime start, DateTime end)
        {
            await Task.CompletedTask;
            return new List<AnalystMeeting>();
        }

        public bool Exists(string calendarId)
        {
            return true;
        }
    }
}