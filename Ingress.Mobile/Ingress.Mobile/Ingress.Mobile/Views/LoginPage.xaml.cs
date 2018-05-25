using Ingress.Mobile.Helpers;
using Ingress.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ingress.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly LoginViewModel _model;

        public LoginPage(LoginViewModel model)
        {
            InitializeComponent();

            this.SetPlatformSpecificProperties();
            
            _model = model;
            BindingContext = _model;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _model.Start();
        }
    }
}