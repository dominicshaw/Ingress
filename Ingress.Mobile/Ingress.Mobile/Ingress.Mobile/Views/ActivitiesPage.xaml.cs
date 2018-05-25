using System;
using Ingress.DTOs;
using Ingress.Mobile.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ingress.Mobile.ViewModels;

namespace Ingress.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivitiesPage : ContentPage
    {
        private readonly ActivitiesViewModel _viewModel;
        private readonly ActivityNavigationManager _navManager;

        public ActivitiesPage()
        {
            InitializeComponent();

            _viewModel = new ActivitiesViewModel();

            _navManager = new ActivityNavigationManager(this, _viewModel);

            BindingContext = _viewModel;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is ActivityDTO item))
                return;

            await Navigation.PushAsync(new ActivityPage(item));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("ADD INTERACTION", "Cancel", null, "Company Meeting", "Analyst Meeting", "Phone Call", "Email", "Model");

            if (string.IsNullOrEmpty(action) || action == "Cancel")
                return;

            switch (action)
            {
                case "Company Meeting":
                    await Navigation.PushAsync(new ActivityPage(new CompanyMeetingDTO { Username = Singleton.Instance.Username }));
                    break;
                case "Analyst Meeting":
                    await Navigation.PushAsync(new ActivityPage(new AnalystMeetingDTO { Username = Singleton.Instance.Username }));
                    break;
                case "Phone Call":
                    await Navigation.PushAsync(new ActivityPage(new PhoneCallDTO { Username = Singleton.Instance.Username }));
                    break;
                case "Email":
                    await Navigation.PushAsync(new ActivityPage(new BrokerEmailDTO { Username = Singleton.Instance.Username }));
                    break;
                case "Model":
                    await Navigation.PushAsync(new ActivityPage(new ModelAccessDTO { Username = Singleton.Instance.Username }));
                    break;
                default:
                    Reporter.ReportException(new Exception("Could not find settings option '" + action + "'."));
                    break;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Items.Count == 0)
                _viewModel.LoadItemsCommand.Execute(null);
        }
    }
}