using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Ingress.DTOs;
using Ingress.Mobile.Annotations;

namespace Ingress.Mobile.ViewModels
{
    public sealed class ActivityDetailViewModel : INotifyPropertyChanged
    {
        private readonly ActivityDTO _model;
        
        public int ActivityID => _model.ActivityID;
        public DateTime InsertedAt => _model.InsertedAt;

        public string Username
        {
            get => _model.Username;
            set
            {
                if (value == _model.Username) return;
                _model.Username = value;
                OnPropertyChanged();
            }
        }
        public string Subject
        {
            get => _model.Subject;
            set
            {
                if (value == _model.Subject) return;
                _model.Subject = value;
                OnPropertyChanged();
            }
        }
        public DateTime DateStart
        {
            get => _model.DateStart;
            set
            {
                _model.DateStart = new DateTime(value.Year, value.Month, value.Day, TimeStart.Hours, TimeStart.Minutes, TimeStart.Seconds);
                OnPropertyChanged();
                OnPropertyChanged(nameof(TimeStart));
            }
        }
        public DateTime DateEnd
        {
            get => _model.DateEnd;
            set
            {
                _model.DateEnd = new DateTime(value.Year, value.Month, value.Day, TimeEnd.Hours, TimeEnd.Minutes, TimeEnd.Seconds);
                OnPropertyChanged();
                OnPropertyChanged(nameof(TimeEnd));
            }
        }
        public TimeSpan TimeStart
        {
            get => new TimeSpan(_model.DateStart.Hour, _model.DateStart.Minute, _model.DateStart.Second);
            set
            {
                _model.DateStart = new DateTime(_model.DateStart.Year, _model.DateStart.Month, _model.DateStart.Day, value.Hours, value.Minutes, value.Seconds);
                OnPropertyChanged();
                OnPropertyChanged(nameof(DateStart));
            }
        }
        public TimeSpan TimeEnd
        {
            get => new TimeSpan(_model.DateEnd.Hour, _model.DateEnd.Minute, _model.DateEnd.Second);
            set
            {
                _model.DateEnd = new DateTime(_model.DateEnd.Year, _model.DateEnd.Month, _model.DateEnd.Day, value.Hours, value.Minutes, value.Seconds);
                OnPropertyChanged();
                OnPropertyChanged(nameof(DateEnd));
            }
        }
        public decimal? BrokerId
        {
            get => _model.BrokerId;
            set
            {
                if (value == _model.BrokerId) return;
                _model.BrokerId = value;
                OnPropertyChanged();
            }
        }
        public string BrokerName
        {
            get => _model.BrokerName;
            set
            {
                if (value == _model.BrokerName) return;
                _model.BrokerName = value;
                OnPropertyChanged();
            }
        }
        public int? Rating
        {
            get => _model.Rating;
            set
            {
                if (value == _model.Rating) return;
                _model.Rating = value;
                OnPropertyChanged();
            }
        }
        public string Comments
        {
            get => _model.Comments;
            set
            {
                if (value == _model.Comments) return;
                _model.Comments = value;
                OnPropertyChanged();
            }
        }
        public string PushOrPull
        {
            get => _model.PushOrPull;
            set
            {
                if (value == _model.PushOrPull) return;
                _model.PushOrPull = value;
                OnPropertyChanged();
            }
        }

        public ActivityDetailViewModel(ActivityDTO model)
        {
            _model = model;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
