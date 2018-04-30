using System;
using Ingress.Data.Interfaces;
using Ingress.Data.Models;

namespace Ingress.WPF.ViewModels.Data
{
    public sealed class AnalystMeetingViewModel : ActivityViewModel
    {
        private readonly AnalystMeeting _activity;

        public AnalystMeetingViewModel(IActivityRepository repo, AnalystMeeting activity) : base(repo, activity)
        {
            _activity = activity;
        }

        public string GlobalID => _activity.GlobalID;
        public string ConvoID => _activity.ConvoID;
        public string Organiser => _activity.Organiser;
        public string Categories => _activity.Categories;
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
        public bool? IsConference
        {
            get => _activity.IsConference;
            set
            {
                if (value == _activity.IsConference) return;
                _activity.IsConference = value;
                OnPropertyChanged();
            }
        }
        public string CalID => _activity.CalID;
    }
}