using Ingress.DTOs;
using Ingress.Mobile.Helpers;
using Ingress.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ingress.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectBrokerPage : ContentPage
    {
        private readonly SelectBrokerViewModel _model;

        public SelectBrokerPage(ActivityDTO dto)
        {
            InitializeComponent();

            _model = new SelectBrokerViewModel(dto);
            BindingContext = _model;
            
            this.SetBackText();
        }  

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _model.SearchCommand.Execute(null);
        }  
    }
}