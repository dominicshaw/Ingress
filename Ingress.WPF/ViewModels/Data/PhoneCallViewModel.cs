using System;
using System.ComponentModel.DataAnnotations;
using Ingress.Data.Models;

namespace Ingress.WPF.ViewModels.Data
{
    public sealed class PhoneCallViewModel : ActivityViewModel
    {
        private readonly PhoneCall _activity;

        public PhoneCallViewModel(PhoneCall activity) : base(activity)
        {
            _activity = activity;
        }
        
        [Required(ErrorMessage = "You must enter an length for this phone call.")]
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
        [Required(ErrorMessage = "You must enter an analyst for this phone call.")]
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
        
        public override string Type => "Phone Call";
        public override Activity GetModel() => _activity;
    }
}