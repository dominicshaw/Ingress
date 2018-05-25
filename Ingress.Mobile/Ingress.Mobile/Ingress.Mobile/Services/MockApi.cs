using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ingress.DTOs;

[assembly: Xamarin.Forms.Dependency(typeof(Ingress.Mobile.Services.MockApi))]
namespace Ingress.Mobile.Services
{
    public class MockApi : IApi
    {
        private readonly List<ActivityDTO> _items;

        public MockApi()
        {
            _items = new List<ActivityDTO>();

            var mockItems = new List<ActivityDTO>
            {
                new CompanyMeetingDTO { ActivityID = 1, CalID = Guid.NewGuid().ToString(), Subject = "First item", Comments="This is an item description." },
                new CompanyMeetingDTO { ActivityID = 2, CalID = Guid.NewGuid().ToString(), Subject = "Second item", Comments="This is an item description." },
                new CompanyMeetingDTO { ActivityID = 3, CalID = Guid.NewGuid().ToString(), Subject = "Third item", Comments="This is an item description." },
                new CompanyMeetingDTO { ActivityID = 4, CalID = Guid.NewGuid().ToString(), Subject = "Fourth item", Comments="This is an item description." },
                new CompanyMeetingDTO { ActivityID = 5, CalID = Guid.NewGuid().ToString(), Subject = "Fifth item", Comments="This is an item description." },
                new CompanyMeetingDTO { ActivityID = 6, CalID = Guid.NewGuid().ToString(), Subject = "Sixth item", Comments="This is an item description." },
            };

            foreach (var item in mockItems)
            {
                _items.Add(item);
            }
        }

        public Task<bool> CheckLogin(string username, string password, CancellationToken token)
        {
            return Task.FromResult(true);
        }

        public async Task<bool> SaveActivity(ActivityDTO item, CancellationToken token)
        {
            _items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteActivity(int id)
        {
            var item = _items.FirstOrDefault(arg => arg.ActivityID == id);
            _items.Remove(item);

            return await Task.FromResult(true);
        }

        public async Task<ActivityDTO> GetActivity(int id)
        {
            return await Task.FromResult(_items.FirstOrDefault(s => s.ActivityID == id));
        }

        public async Task<IEnumerable<ActivityDTO>> GetActivitiesForUser(CancellationToken token, bool forceRefresh = false)
        {
            return await Task.FromResult(_items);
        }

        public async Task<IEnumerable<BrokerDTO>> GetBrokers()
        {
            return await Task.FromResult(new List<BrokerDTO>
            {
                new BrokerDTO {BrokerID = 1, Name = "A broker"}
            });
        }

        public async Task<IEnumerable<string>> GetAnalysts()
        {
            return await Task.FromResult(new List<string>
            {
                "An analyst"
            });
        }

        public async Task<IEnumerable<string>> GetUsers()
        {
            return await Task.FromResult(new List<string>
            {
                "Dominic Shaw"
            });
        }
    }
}