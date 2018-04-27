using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Ingress.Annotations;
using Ingress.Data.Models;

namespace Ingress.ViewModels
{
    public sealed class AnalystMeetingViewModel : INotifyPropertyChanged
    {
        private readonly AnalystMeeting _analystMeeting;

        public AnalystMeetingViewModel(AnalystMeeting analystMeeting)
        {
            _analystMeeting = analystMeeting;
        }

        public int ActivityID => _analystMeeting.ActivityID;
        public string GlobalID => _analystMeeting.GlobalID;
        public string ConvoID => _analystMeeting.ConvoID;
        public DateTime InsertedAt => _analystMeeting.InsertedAt;
        public string Username => _analystMeeting.Username;
        public string Organiser => _analystMeeting.Organiser;
        public string Subject => _analystMeeting.Subject;
        public DateTime DateStart => _analystMeeting.DateStart;
        public DateTime DateEnd => _analystMeeting.DateEnd;
        public string Categories => _analystMeeting.Categories;
        public string TimeTaken
        {
            get => _analystMeeting.TimeTaken;
            set
            {
                if (value == _analystMeeting.TimeTaken) return;
                _analystMeeting.TimeTaken = value;
                OnPropertyChanged();
            }
        }
        public decimal? BrokerId
        {
            get => _analystMeeting.BrokerId;
            set
            {
                if (value == _analystMeeting.BrokerId) return;
                _analystMeeting.BrokerId = value;
                OnPropertyChanged();
            }
        }
        public string BrokerName
        {
            get => _analystMeeting.BrokerName;
            set
            {
                if (value == _analystMeeting.BrokerName) return;
                _analystMeeting.BrokerName = value;
                OnPropertyChanged();
            }
        }
        public string Analyst
        {
            get => _analystMeeting.Analyst;
            set
            {
                if (value == _analystMeeting.Analyst) return;
                _analystMeeting.Analyst = value;
                OnPropertyChanged();
            }
        }
        public int? Rating
        {
            get => _analystMeeting.Rating;
            set
            {
                if (value == _analystMeeting.Rating) return;
                _analystMeeting.Rating = value;
                OnPropertyChanged();
            }
        }
        public string Comments
        {
            get => _analystMeeting.Comments;
            set
            {
                if (value == _analystMeeting.Comments) return;
                _analystMeeting.Comments = value;
                OnPropertyChanged();
            }
        }
        public bool? IsConference
        {
            get => _analystMeeting.IsConference;
            set
            {
                if (value == _analystMeeting.IsConference) return;
                _analystMeeting.IsConference = value;
                OnPropertyChanged();
            }
        }
        public string PushOrPull
        {
            get => _analystMeeting.PushOrPull;
            set
            {
                if (value == _analystMeeting.PushOrPull) return;
                _analystMeeting.PushOrPull = value;
                OnPropertyChanged();
            }
        }
        public string CalID => _analystMeeting.CalID;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}