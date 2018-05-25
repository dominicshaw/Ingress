using System.Threading.Tasks;
using Ingress.Mobile.Helpers;
using Ingress.Mobile.MVVM;
using Ingress.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ingress.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            Messenger.Instance.Register("Reset", async () =>
            {
                CurrentPage = activitiesTab;
                await CheckLogin();
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await CheckLogin();
        }

        private async Task CheckLogin()
        {
            if (string.IsNullOrWhiteSpace(Singleton.Instance.Username))
            {
                var model = new LoginViewModel();

                async void ModelNotification(string title, string message)
                {
                    await DisplayAlert(title, message, "Ok");
                }
                
                async void ModelSuccess(object sender, System.EventArgs e)
                {
                    await Navigation.PopModalAsync(true);

                    if (model != null)
                    {
                        model.Notification -= ModelNotification;
                        model.Success -= ModelSuccess;

                        model.Dispose();
                        model = null;
                    }
                }

                model.Notification += ModelNotification;
                model.Success += ModelSuccess;

                await Navigation.PushModalAsync(new LoginPage(model));
            }
        }
    }
}