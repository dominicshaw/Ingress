using System;
using System.Collections.ObjectModel;
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
    public class ActivitiesViewModel : BaseViewModel
    {
        public ICommand LoadItemsCommand { get; }

        public ObservableCollection<ActivityDTO> Items { get; } = new ObservableCollection<ActivityDTO>();

        public ActivitiesViewModel()
        {
            Title = "Your Interactions";
            LoadItemsCommand = new Command(async () => await LoadActivities());

            Messenger.Instance.Register<ActivityDTO>("AddItem", dto => { Items.Insert(0, dto); });
        }

        private async Task LoadActivities()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();

                var cancel = new CancellationTokenSource(TimeSpan.FromSeconds(5));

                foreach (var item in await Api.GetActivitiesForUser(cancel.Token, true))
                    Items.Add(item);
            }
            catch (Exception ex) when (ex is WebException || ex is HttpRequestException || ex is TaskCanceledException)
            {
                Reporter.TrackDebug($"Web Request Error Saving Activity ({ex.Message})");
                Messenger.Instance.NotifyColleagues("Notification", new Notification("Load Failed", "Please try again when you have a better signal."));
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