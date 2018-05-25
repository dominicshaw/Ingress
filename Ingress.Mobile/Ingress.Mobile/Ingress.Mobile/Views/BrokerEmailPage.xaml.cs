using Ingress.DTOs;
using Ingress.Mobile.Helpers;
using Ingress.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ingress.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BrokerEmailPage : ContentPage
	{
	    public BrokerEmailPage(BrokerEmailDTO dto)
	    {
	        InitializeComponent();
	        BindingContext = new BrokerEmailViewModel(dto);

	        this.SetBackText();
	    }
	}
}