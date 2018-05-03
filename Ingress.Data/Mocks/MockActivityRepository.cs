﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ingress.Data.Interfaces;
using Ingress.Data.Models;
using JetBrains.Annotations;

namespace Ingress.Data.Mocks
{
    [UsedImplicitly]
    public class MockActivityRepository : IActivityRepository
    {
        private readonly List<Activity> _activities = new List<Activity>() { new PhoneCall() { ActivityID = 1 }, new AnalystMeeting() { ActivityID = 2 }, new CompanyMeeting() { ActivityID = 3 } };

        public async Task<List<Activity>> GetAll()
        {
            await Task.CompletedTask;
            return _activities;
        }

        public async Task<Activity> GetById(string id)
        {
            await Task.CompletedTask;
            return new PhoneCall();
        }

        public void Create(Activity entity)
        {
            entity.ActivityID = _activities.Max(x => x.ActivityID) + 1;
            _activities.Add(entity);
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
