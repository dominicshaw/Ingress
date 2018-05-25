using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Ingress.DTOs;
using Ingress.Mobile.Helpers;
using Ingress.Mobile.MVVM;
using Xamarin.Forms;

namespace Ingress.Mobile.ViewModels
{
    public abstract class ActivityBaseViewModel : BaseViewModel
    {
        private readonly ActivityDTO _model;

        private string _validationErrors;

        public ICommand SaveCommand => new Command(async () => await Save());

        protected ActivityBaseViewModel(ActivityDTO model)
        {
            _model = model;
        }

        protected abstract bool Validate();
        protected abstract void Commit();

        public string ValidationErrors
        {
            get => _validationErrors;
            set
            {
                if (value == _validationErrors) return;
                _validationErrors = value;
                OnPropertyChanged();
            }
        }

        private async Task Save()
        {
            if (Validate())
            {
                Commit();

                try
                {
                    IsBusy = true;

                    var cancel = new CancellationTokenSource(TimeSpan.FromSeconds(10));

                    var success = await Api.SaveActivity(_model, cancel.Token);

                    if (success)
                    {
                        Messenger.Instance.NotifyColleagues("AddItem", _model);
                        
                        Reporter.Track("SaveActivitySuccess", new Dictionary<string, string>
                        {
                            {"Type", _model.GetType().ToString()},
                            {"Broker", _model.BrokerName},
                            {"Subject", _model.Subject},
                            {"Username", _model.Username}
                        });
                    }
                    else
                    {
                        Messenger.Instance.NotifyColleagues("Notification", new Notification("Save Failed", "Please try again when you have a better signal."));
                        Messenger.Instance.NotifyColleagues("ContinueToTypeSpecific", _model);

                        Reporter.Track("SaveActivityFailure", new Dictionary<string, string>
                        {
                            {"Type", _model.GetType().ToString()},
                            {"Broker", _model.BrokerName},
                            {"Subject", _model.Subject},
                            {"Username", _model.Username}
                        });
                    }
                }
                catch (Exception ex) when (ex is WebException || ex is HttpRequestException || ex is TaskCanceledException)
                {
                    Reporter.TrackDebug($"Web Request Error Saving Activity ({ex.Message})");
                    Messenger.Instance.NotifyColleagues("Notification", new Notification("Save Failed", "Please try again when you have a better signal."));
                }
                catch (Exception ex)
                {
                    Reporter.ReportException(ex);
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }
    }
}