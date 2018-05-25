using Ingress.DTOs;
using Ingress.Mobile.Helpers;
using Ingress.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ingress.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModelAccessPage : ContentPage
    {
        public ModelAccessPage(ModelAccessDTO dto)
        {
            InitializeComponent();
            BindingContext = new ModelAccessViewModel(dto);
            
            this.SetBackText();
        }
    }
}