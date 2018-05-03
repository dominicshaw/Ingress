using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;
using DevExpress.Mvvm;
using Ingress.Data.DataSources;
using Ingress.Data.Models;
using JetBrains.Annotations;

namespace Ingress.WPF.ViewModels.Data
{
    public abstract class ActivityViewModel : SelectableViewModel, INotifyPropertyChanged, IDataErrorInfo
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
                    BrokerId = value.ID;
                    BrokerName = value.Name;
                }
            }
        }

        public decimal? BrokerId
        {
            get => _activity.BrokerId;
            set
            {
                if (value == _activity.BrokerId) return;
                _activity.BrokerId = value;
                OnPropertyChanged();
            }
        }
        public string BrokerName
        {
            get => _activity.BrokerName;
            set
            {
                if (value == _activity.BrokerName) return;
                _activity.BrokerName = value;
                OnPropertyChanged();
            }
        }
        
        [Required(ErrorMessage = "You must select a rating for the activity.")]
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
        [Required(ErrorMessage = "You must select whether the activity was push or pull.")]
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
        public abstract string Type { get; }

        public abstract Activity GetModel();
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        string IDataErrorInfo.this[string columnName] => IDataErrorInfoHelper.GetErrorText(this, columnName);

        public string Error
        {
            get
            {
                var errors = new StringBuilder();

                foreach (var prop in GetType().GetProperties())
                {
                    foreach (ValidationAttribute att in prop.GetCustomAttributes(typeof(ValidationAttribute), true))
                    {
                        if(!att.IsValid(prop.GetValue(this)))
                        {
                            errors.AppendLine(att.ErrorMessage);
                        }
                    }
                }

                return errors.ToString().Trim();
            }
        }

        public bool IsValid
        {
            get
            {
                foreach (var prop in GetType().GetProperties())
                    foreach (ValidationAttribute att in prop.GetCustomAttributes(typeof(ValidationAttribute), true))
                        if (!att.IsValid(prop.GetValue(this)))
                            return false;
                return true;
            }
        }
    }
}