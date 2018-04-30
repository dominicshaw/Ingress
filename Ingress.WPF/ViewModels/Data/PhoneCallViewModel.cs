using System;
using Ingress.Data.Interfaces;
using Ingress.Data.Models;

namespace Ingress.WPF.ViewModels.Data
{
    public sealed class PhoneCallViewModel : ActivityViewModel
    {
        private readonly PhoneCall _activity;

        public PhoneCallViewModel(IActivityRepository repo, PhoneCall activity) : base(repo, activity)
        {
            _activity = activity;
        }

        public TimeSpan? TimeTaken
        {
            get
            {
                if(_activity.TimeTaken == null)
                    return null;

                return TimeSpan.Parse(_activity.TimeTaken);
            }
            set
            {
                if (value == null)
                    _activity.TimeTaken = null;

                _activity.TimeTaken = value.ToString();
                OnPropertyChanged();
            }
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