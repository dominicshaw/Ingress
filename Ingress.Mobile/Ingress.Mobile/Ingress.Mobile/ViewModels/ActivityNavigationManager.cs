using System;
using System.Threading.Tasks;
using Ingress.DTOs;
using Ingress.Mobile.Helpers;
using Ingress.Mobile.MVVM;
using Ingress.Mobile.Services;
using Ingress.Mobile.Views;

namespace Ingress.Mobile.ViewModels
{
    public class ActivityNavigationManager
    {
        private readonly ActivitiesPage _page;
        private readonly ActivitiesViewModel _viewModel;

        public ActivityNavigationManager(ActivitiesPage nav, ActivitiesViewModel viewModel)
        {
            _page = nav;
            _viewModel = viewModel;

            Messenger.Instance.Register<ActivityDTO>("ContinueToBroker"      , async dto => { await _page.Navigation.PushAsync(new SelectBrokerPage(dto)); });
            Messenger.Instance.Register<ActivityDTO>("ContinueToTypeSpecific", async dto => { await GotoAppropriateTypePage(dto); });
            Messenger.Instance.Register<ActivityDTO>("AddItem"               , async dto => { await PopAllWindows(); });
            Messenger.Instance.Register<Notification>("Notification"         , async n   => { await Notify(n); });
        }

        private async Task GotoAppropriateTypePage(ActivityDTO dto)
        {
            switch (dto)
            {
                case CompanyMeetingDTO activity:
                    await _page.Navigation.PushAsync(new CompanyMeetingPage(activity));
                    break;
                case AnalystMeetingDTO activity:
                    await _page.Navigation.PushAsync(new AnalystMeetingPage(activity));
                    break;
                case PhoneCallDTO activity:
                    await _page.Navigation.PushAsync(new PhoneCallPage(activity));
                    break;
                case BrokerEmailDTO activity:
                    await _page.Navigation.PushAsync(new BrokerEmailPage(activity));
                    break;
                case ModelAccessDTO activity:
                    await _page.Navigation.PushAsync(new ModelAccessPage(activity));
                    break;
            }
        }

        private async Task PopAllWindows()
        {
            try
            {
                while (_page.Navigation.ModalStack.Count > 0)
                    await _page.Navigation.PopModalAsync(_page.Navigation.ModalStack.Count == 1);

                while (_page.Navigation.NavigationStack.Count > 1)
                    await _page.Navigation.PopAsync(_page.Navigation.NavigationStack.Count == 2);

                await Notify(new Notification("Save Successful", "Your interaction has been saved."));

                //_viewModel.LoadItemsCommand.Execute(null);
            }
            catch (Exception ex)
            {
                Reporter.ReportException(ex);
            }
        }

        private async Task Notify(Notification notification)
        {
            if (string.IsNullOrEmpty(notification.Cancel))
                await _page.DisplayAlert(notification.Title, notification.Message, notification.Accept);
            else
                await _page.DisplayAlert(notification.Title, notification.Message, notification.Accept, notification.Cancel);
        }
    }
}