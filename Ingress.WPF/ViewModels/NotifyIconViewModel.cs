using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using Ingress.Data.Models;

namespace Ingress.WPF.ViewModels
{
    public class NotifyIconViewModel
    {
        public ICommand ShowWindowCommand => new DelegateCommand(() => Application.Current?.MainWindow?.Show(), () => !IsDisplayed());
        public ICommand HideWindowCommand => new DelegateCommand(() => Application.Current?.MainWindow?.Hide(), IsDisplayed);
        public ICommand ExitApplicationCommand => new DelegateCommand(() => Application.Current.Shutdown());
        
        private static bool IsDisplayed()
        {
            return Application.Current?.MainWindow?.IsVisible ?? false;
        }

        public ICommand AddAnalystMeetingCommand => new DelegateCommand(() => Add(ActivityTypes.AnalystMeeting));
        public ICommand AddCompanyMeetingCommand => new DelegateCommand(() => Add(ActivityTypes.CompanyMeeting));
        public ICommand AddPhoneCallCommand      => new DelegateCommand(() => Add(ActivityTypes.PhoneCall));
        public ICommand AddBrokerEmailCommand    => new DelegateCommand(() => Add(ActivityTypes.BrokerEmail));
        public ICommand AddModelAccessCommand    => new DelegateCommand(() => Add(ActivityTypes.ModelAccess));
        
        private static void Add(ActivityType activityType)
        {
            Messenger.Default.Send(new NewActivityRequest(activityType));
        }
    }
}