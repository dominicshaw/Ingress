using System;
using Ingress.DTOs;
using Ingress.Mobile.Helpers;
using Ingress.Mobile.MVVM;
using Ingress.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ingress.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityPage : ContentPage
    {
        private readonly ActivityDTO _model;

        public ActivityPage(ActivityDTO dto)
        {
            InitializeComponent();

            _model = dto;

            BindingContext = new ActivityDetailViewModel(dto);

            this.SetBackText();
        }

        private async void Continue_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_model.Subject))
            {
                await DisplayAlert("Validation", "Please enter a subject.", "Ok");
                return;
            }

            if (!_model.Rating.HasValue)
            {
                await DisplayAlert("Validation", "Please select rating.", "Ok");
                return;
            }

            if (string.IsNullOrWhiteSpace(_model.PushOrPull))
            {
                await DisplayAlert("Validation", "Please select if this was push or pull.", "Ok");
                return;
            }

            Messenger.Instance.NotifyColleagues("ContinueToBroker", _model);
        }
    }
}