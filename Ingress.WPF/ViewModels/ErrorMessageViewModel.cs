using System;
using System.Windows.Input;
using DevExpress.Mvvm;
using Ingress.WPF.ViewModels.MessengerCommands;

namespace Ingress.WPF.ViewModels
{
    public class ErrorMessageViewModel : SelectableViewModel
    {
        public ICommand OkCommand => new DelegateCommand(() => Messenger.Default.Send(new NavigationCommand(Where.List)));

        public ErrorMessageViewModel(Exception exception)
        {
            ErrorMessage = exception.Message;
        }

        public string ErrorMessage { get; }
    }
}
