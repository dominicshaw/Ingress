using System.ComponentModel.DataAnnotations;
using Ingress.Data.Models;

namespace Ingress.WPF.ViewModels.Data
{
    public sealed class BrokerEmailViewModel : ActivityViewModel
    {
        private readonly BrokerEmail _activity;

        public BrokerEmailViewModel(BrokerEmail activity) : base(activity)
        {
            _activity = activity;
        }
        
        [Required(ErrorMessage = "You must enter an analyst for this email.")]
        public string Analyst
        {
            get => _activity.Analyst;
            set
            {
                if (value == _activity.Analyst) return;
                _activity.Analyst = value;
                OnPropertyChanged();
            }
        }
        
        public override string Type => "Email";
        public override Activity GetModel() => _activity;
    }
}