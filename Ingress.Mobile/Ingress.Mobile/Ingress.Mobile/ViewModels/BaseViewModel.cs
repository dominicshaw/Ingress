using System.ComponentModel;
using System.Runtime.CompilerServices;
using Ingress.Mobile.Annotations;
using Xamarin.Forms;
using Ingress.Mobile.Services;

namespace Ingress.Mobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private bool _isBusy;
        private string _title;

        protected IApi Api => DependencyService.Get<IApi>() ?? new MockApi();

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (value == _isBusy) return;
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                if (value == _title) return;
                _title = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
