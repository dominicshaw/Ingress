using System.DirectoryServices.AccountManagement;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using Ingress.Data.Models;
using Ingress.WPF.ViewModels.Data;
using Ingress.WPF.ViewModels.MessengerCommands;

namespace Ingress.WPF.ViewModels
{
    public class GlobalCommandsViewModel
    {
        private readonly string _username;

        public GlobalCommandsViewModel()
        {
            _username = UserPrincipal.Current.DisplayName;
        }

        public ICommand ShowWindowCommand => new DelegateCommand(() => Application.Current?.MainWindow?.Show(), () => !IsDisplayed());
        public ICommand HideWindowCommand => new DelegateCommand(() => Application.Current?.MainWindow?.Hide(), IsDisplayed);
        public ICommand ExitApplicationCommand => new DelegateCommand(() => Application.Current.Shutdown());
        
        private static bool IsDisplayed()
        {
            return Application.Current?.MainWindow?.IsVisible ?? false;
        }

        public ICommand AddAnalystMeetingCommand => new DelegateCommand(() => Messenger.Default.Send(new NavigationCommand(Where.Activity, new AnalystMeetingViewModel(new AnalystMeeting { Username = _username } ))));
        public ICommand AddCompanyMeetingCommand => new DelegateCommand(() => Messenger.Default.Send(new NavigationCommand(Where.Activity, new CompanyMeetingViewModel(new CompanyMeeting { Username = _username } ))));
        public ICommand AddPhoneCallCommand      => new DelegateCommand(() => Messenger.Default.Send(new NavigationCommand(Where.Activity, new PhoneCallViewModel     (new PhoneCall      { Username = _username } ))));
        public ICommand AddBrokerEmailCommand    => new DelegateCommand(() => Messenger.Default.Send(new NavigationCommand(Where.Activity, new BrokerEmailViewModel   (new BrokerEmail    { Username = _username } ))));
        public ICommand AddModelAccessCommand    => new DelegateCommand(() => Messenger.Default.Send(new NavigationCommand(Where.Activity, new ModelAccessViewModel   (new ModelAccess    { Username = _username } ))));

        public ICommand ListViewCommand => new DelegateCommand(() => Messenger.Default.Send(new NavigationCommand(Where.List)));
    }
}