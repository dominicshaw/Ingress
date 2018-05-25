using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Ingress.DTOs;
using Ingress.Mobile.Helpers;
using Ingress.Mobile.MVVM;
using Ingress.Mobile.Services;
using Xamarin.Forms;

namespace Ingress.Mobile.ViewModels
{
    public class SelectBrokerViewModel : BaseViewModel
    {
        private string _searchText;
        private bool _working;

        public ICommand SearchCommand => new Command(async () => await Search());
        public ICommand SkipCommand => new Command(Skip);
        public ICommand RefreshCommand => new Command(async () => await Refresh());
        public ICommand SelectedCommand => new Command<object>(Selected);
        
        public ActivityDTO Activity { get; }

        public ObservableCollection<BrokerDTO> Results { get; } = new ObservableCollection<BrokerDTO>();

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (value == _searchText) return;
                _searchText = value;
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
            }
        }

        public SelectBrokerViewModel(ActivityDTO activity)
        {
            Activity = activity;
            SearchText = activity.BrokerName;
        }

        private async Task Search()
        {
            Working = true;

            Reporter.TrackDebug($"Searching Brokers: \'{SearchText}\'.");

            try
            {
                IList<BrokerDTO> results = new List<BrokerDTO>();

                await Task.Run(() =>
                {
                    using (var db = new LocalDb())
                    {
                        results = db.SearchBrokers(SearchText);
                    }
                });

                Results.Clear();
                foreach (var r in results)
                    Results.Add(r);
            }
            catch (Exception ex)
            {
                Reporter.ReportException(ex);
            }
            finally
            {
                Working = false;
            }
        }

        private async Task Refresh()
        {
            Working = true;

            try
            {
                var brokers = (await Api.GetBrokers()).ToList();

                await Task.Run(() =>
                {
                    using (var db = new LocalDb())
                    {
                        db.SaveBrokers(brokers);
                        Reporter.TrackDebug("Saved " + brokers.Count + " brokers.");
                    }
                });

                await Search();
            }
            catch (Exception ex)
            {
                Reporter.ReportException(ex);
            }
            finally
            {
                Working = false;
            }
        }

        private void Skip()
        {
            Messenger.Instance.NotifyColleagues("ContinueToTypeSpecific", Activity);
        }

        private void Selected(object item)
        {
            try
            {
                if (item is BrokerDTO broker)
                {
                    Activity.BrokerId = broker.BrokerID;
                    Activity.BrokerName = broker.Name;

                    Messenger.Instance.NotifyColleagues("ContinueToTypeSpecific", Activity);
                }
            }
            catch (Exception ex)
            {
                Reporter.ReportException(ex);
            }
        }
    }
}