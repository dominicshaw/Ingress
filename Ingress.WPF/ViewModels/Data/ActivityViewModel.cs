using System;
using System.ComponentModel.DataAnnotations;
using Ingress.Data.DataSources;
using Ingress.Data.Models;
using Ingress.WPF.ViewModels.Attributes;

namespace Ingress.WPF.ViewModels.Data
{
    public abstract class ActivityViewModel : SelectableViewModel
    {
        private readonly Activity _activity;

        private Broker _broker;
        private bool _skipped;

        protected ActivityViewModel(Activity activity)
        {
            _activity = activity;
        }

        public int ActivityID => _activity.ActivityID;
        public DateTime InsertedAt => _activity.InsertedAt;
        [Required(ErrorMessage = "You must enter a user to associate this activity with.")]
        public string Username => _activity.Username;
        [Required(ErrorMessage = "You must enter a subject for the activity.")]
        public string Subject
        {
            get => _activity.Subject;
            set
            {
                if (value == _activity.Subject) return;
                _activity.Subject = value;
                OnPropertyChanged();
            }
        }
        public DateTime DateStart
        {
            get => _activity.DateStart;
            set
            {
                if (value == _activity.DateStart) return;
                _activity.DateStart = value;
                OnPropertyChanged();
            }
        }
        public DateTime DateEnd
        {
            get => _activity.DateEnd;
            set
            {
                if (value == _activity.DateEnd) return;
                _activity.DateEnd = value;
                OnPropertyChanged();
            }
        }

        public decimal? BrokerId => _activity.BrokerId;

        [RequiredIf("RequiresBroker", true, ErrorMessage = "You must select a broker.")]
        public Broker Broker
        {
            get => _broker;
            set
            {
                if (Equals(value, _broker)) return;
                _broker = value;
                OnPropertyChanged();

                if (value != null)
                {
#pragma warning disable INPC003
                    _activity.BrokerId = value.ID;
#pragma warning restore INPC003
                    _activity.BrokerName = value.Name;
                }
            }
        }

        [RequiredIf("RequiresRating", true, ErrorMessage = "You must select a rating for the activity.")]
        public int? Rating
        {
            get => _activity.Rating;
            set
            {
                if (value == _activity.Rating) return;
                _activity.Rating = value;
                OnPropertyChanged();
            }
        }
        public string Comments
        {
            get => _activity.Comments;
            set
            {
                if (value == _activity.Comments) return;
                _activity.Comments = value;
                OnPropertyChanged();
            }
        }
        [RequiredIf("RequiresPushPull", true, ErrorMessage = "You must select whether the activity was push or pull.")]
        public string PushOrPull
        {
            get => _activity.PushOrPull;
            set
            {
                if (value == _activity.PushOrPull) return;
                _activity.PushOrPull = value;
                OnPropertyChanged();
            }
        }
        public virtual bool Skipped
        {
            get => _skipped;
            set
            {
                if (value == _skipped) return;
                _skipped = value;
                OnPropertyChanged();
            }
        }

        public virtual bool RequiresBroker { get; } = false;
        public virtual bool RequiresRating { get; } = true;
        public virtual bool RequiresPushPull { get; } = true;
        public abstract string Type { get; }

        public abstract Activity GetModel();
    }
}