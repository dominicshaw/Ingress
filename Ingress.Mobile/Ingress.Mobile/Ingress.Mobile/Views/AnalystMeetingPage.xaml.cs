using Ingress.DTOs;
using Ingress.Mobile.Helpers;
using Ingress.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ingress.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnalystMeetingPage : ContentPage
    {
        public AnalystMeetingPage(AnalystMeetingDTO dto)
        {
            InitializeComponent();
            BindingContext = new AnalystMeetingViewModel(dto);

            this.SetBackText();
        }
    }
}