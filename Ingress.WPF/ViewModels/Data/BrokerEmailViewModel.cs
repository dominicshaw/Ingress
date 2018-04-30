using Ingress.Data.Interfaces;
using Ingress.Data.Models;

namespace Ingress.WPF.ViewModels.Data
{
    public sealed class BrokerEmailViewModel : ActivityViewModel
    {
        private readonly BrokerEmail _activity;

        public BrokerEmailViewModel(IActivityRepository repo, BrokerEmail activity) : base(repo, activity)
        {
            _activity = activity;
        }

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
    }
}