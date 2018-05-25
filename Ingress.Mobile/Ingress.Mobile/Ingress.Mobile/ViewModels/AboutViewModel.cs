using System;
using System.Windows.Input;
using Ingress.Mobile.Helpers;
using Ingress.Mobile.MVVM;
using Xamarin.Forms;

namespace Ingress.Mobile.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            ResetCommand = new Command(() => Reset());
        }

        public ICommand ResetCommand { get; }

        private void Reset()
        {
            Singleton.Instance.Username = string.Empty;
            Messenger.Instance.NotifyColleagues("Reset");
        }
    }
}