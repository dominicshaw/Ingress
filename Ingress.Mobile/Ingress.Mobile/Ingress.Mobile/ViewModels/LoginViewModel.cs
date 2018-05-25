using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Ingress.Mobile.Annotations;
using Ingress.Mobile.Helpers;
using Ingress.Mobile.Services;
using Xamarin.Forms;

namespace Ingress.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel, IDisposable
    {
        private readonly LocalDb _db;
        private readonly IApi _api;
        private string _fullname;
        private string _username;
        private string _password;
        private List<string> _users;
        private bool _working;

        public delegate void NotificationEventHandler(string title, string message);
        public event NotificationEventHandler Notification;
        public event EventHandler Success;

        public ICommand LoginCommand => new Command(async () => await Login(), () => !Working);

        public List<string> Users
        {
            get => _users;
            set
            {
                if (Equals(value, _users)) return;
                _users = value;
                OnPropertyChanged();
            }
        }

        public string Fullname
        {
            get => _fullname;
            set
            {
                if (value == _fullname) return;
                _fullname = value;
                OnPropertyChanged();
            }
        }
        public string Username
        {
            get => _username;
            set
            {
                if (value == _username) return;
                _username = value;
                OnPropertyChanged();

                if (Users != null && Users.Count > 0 && !string.IsNullOrWhiteSpace(_username) && string.IsNullOrWhiteSpace(Fullname))
                {
                    var idx = _username.IndexOf("_", StringComparison.Ordinal);
                    if (idx != -1)
                    {
                        Fullname = Users.Find(x => x.Contains(_username.Substring(0, idx)));
                    }
                }
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                if (value == _password) return;
                _password = value;
                OnPropertyChanged();
            }
        }

        public bool Working
        {
            get => _working;
            set
            {
                if (value == _working) return;
                _working = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoginCommand));

                IsBusy = value;
            }
        }

        public LoginViewModel()
        {
            _db = new LocalDb();
            _api = DependencyService.Get<IApi>() ?? new MockApi();
        }

        public async Task Start()
        {
            Working = true;

            try
            {
                Users = new List<string>(await _api.GetUsers());
            }
            catch (TaskCanceledException)
            {
                Notification?.Invoke("Timeout", "Your request timed out. Please try again and if the problem persists, please contact IT.");
            }
            catch (Exception ex)
            {
                Reporter.ReportException(ex, new Dictionary<string, string> {{"Location", "LoginViewModel.Start"}});
                Notification?.Invoke("Error", ex.Message);
            }
            finally
            {
                Working = false;
            }
        }

        private async Task Login()
        {
            try
            {
                Working = true;

                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));

                var success = await _api.CheckLogin(Username, Password, cts.Token);

                if (success)
                {
                    Singleton.Instance.Username = Fullname;

                    Reporter.Identify(new Dictionary<string, string>
                    {
                        {"Username", Username},
                        {"Fullname", Fullname}
                    });

                    Reporter.Track("Login Success", new Dictionary<string, string>
                    {
                        { "Username", Username },
                        { "Fullname", Fullname }
                    });
                    
                    Working = false;
                    Password = string.Empty;

                    Success?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    Working = false;
                    Password = string.Empty;

                    Reporter.Track("Login Fail", new Dictionary<string, string> { { "Username", Username } });

                    Notification?.Invoke("Login Failed", "You have entered an invalid username or password. Please try again. The username should be in the format \"surname_f\". The password is your Window's password.");
                }
            }
            catch (TaskCanceledException)
            {
                Notification?.Invoke("Timeout", "Your request timed out. Please try again and if the problem persists, please contact IT.");
            }
            catch (Exception ex)
            {
                Reporter.ReportException(ex, new Dictionary<string, string> {{"Location", "LoginViewModel.Login"}});
                Notification?.Invoke("Error", ex.Message);
            }
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}