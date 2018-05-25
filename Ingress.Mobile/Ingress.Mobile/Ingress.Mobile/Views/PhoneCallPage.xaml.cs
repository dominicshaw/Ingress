using Ingress.DTOs;
using Ingress.Mobile.Helpers;
using Ingress.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ingress.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhoneCallPage : ContentPage
    {
        public PhoneCallPage(PhoneCallDTO dto)
        {
            InitializeComponent();
            BindingContext = new PhoneCallViewModel(dto);
            
            this.SetBackText();
        }
    }
}