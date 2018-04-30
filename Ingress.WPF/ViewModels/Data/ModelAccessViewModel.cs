using System;
using Ingress.Data.Interfaces;
using Ingress.Data.Models;

namespace Ingress.WPF.ViewModels.Data
{
    public sealed class ModelAccessViewModel : ActivityViewModel
    {
        private readonly ModelAccess _activity;

        public ModelAccessViewModel(IActivityRepository repo, ModelAccess activity) : base(repo, activity)
        {
            _activity = activity;
        }

        public TimeSpan? TimeTaken
        {
            get => TimeSpan.Parse(_activity.TimeTaken);
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