using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ingress.DTOs;
using Ingress.Mobile.Helpers;
using Newtonsoft.Json;

[assembly: Xamarin.Forms.Dependency(typeof(Ingress.Mobile.Services.HttpApi))]
namespace Ingress.Mobile.Services
{
    public class HttpApi : IApi
    {
        private static readonly HttpClient _client = new HttpClient();

        private const string _server = "http://server.domain.com:8199";

        static HttpApi()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.Timeout = TimeSpan.FromMinutes(20);
        }

        public async Task<bool> CheckLogin(string username, string password, CancellationToken token)
        {
            const string uri = _server + "/Users/Login";

            var response = await _client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(new LoginDTO() {Username = username, PasswordBytes = LoginDTO.Encrypt(password)}), Encoding.UTF8, "application/json"), token);
            
            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                return message.ToUpperInvariant().Contains("SUCCESS");
            }

            Report(response.StatusCode.ToString());
            return false;
        }

        public async Task<bool> SaveActivity(ActivityDTO item, CancellationToken token)
        {
            var uri = $"{_server}/Activities/Save";

            Reporter.Track("SaveActivityCall", new Dictionary<string, string>
            {
                {"Type", item.GetType().ToString()},
                {"Broker", item.BrokerName},
                {"Subject", item.Subject},
                {"Username", item.Username}
            });

            // TypeNameHandling: nice little setting tells the serialiser to add the type of the object to the json 
            // so when it is deserialised it, it deserialised to the right concrete class instead of the base class
            var json = JsonConvert.SerializeObject(item, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });

            var response = await _client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"), token);

            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();

                message = message.Replace("\"", "");

                if (int.TryParse(message, out var newId))
                {
                    item.ActivityID = newId;
                    return true;
                }
                
                Reporter.Track("SaveActivityFailure", new Dictionary<string, string> {{"BadMessage", message}});
                return false;
            }

            Report(response.StatusCode.ToString());
            return false;
        }

        public Task<bool> DeleteActivity(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActivityDTO> GetActivity(int id)
        {
            var uri = $"{_server}/Activities/ById/{id}";

            var result = await Get<ActivityDTO>(uri, CancellationToken.None);

            return result;
        }

        public async Task<IEnumerable<ActivityDTO>> GetActivitiesForUser(CancellationToken token, bool forceRefresh = false)
        {
            if(string.IsNullOrEmpty(Singleton.Instance.Username))
                return new List<ActivityDTO>();

            var uri = $"{_server}/Activities/ByUser/{Singleton.Instance.Username}";

            var result = await Get<List<ActivityDTO>>(uri, token);

            return result.OrderByDescending(x => x.DateStart);
        }

        public async Task<IEnumerable<BrokerDTO>> GetBrokers()
        {
            var uri = $"{_server}/Data/Brokers";

            var result = await Get<List<BrokerDTO>>(uri, CancellationToken.None);

            return result;
        }
        
        public async Task<IEnumerable<string>> GetAnalysts()
        {
            var uri = $"{_server}/Data/Analysts";

            var result = await Get<List<string>>(uri, CancellationToken.None);

            return result;
        }

        public async Task<IEnumerable<string>> GetUsers()
        {
            var uri = $"{_server}/Users/Get";

            var result = await Get<List<string>>(uri, CancellationToken.None);

            return result;
        }

        private static async Task<T> Get<T>(string uri, CancellationToken token)
        {
            var response = await _client.GetAsync(uri, token);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            }

            Report(response.StatusCode + ": " + uri);

            return default(T);
        }

        private static void Report(string message, [CallerMemberName] string propertyName = null)
        {
            var data = new Dictionary<string, string>
            {
                {"Message", message},
                {"CallingMethod", propertyName},
                {"user", Singleton.Instance.Username}
            };

            Reporter.TrackError("WebAPI-Exception", data);
        }
    }
}
