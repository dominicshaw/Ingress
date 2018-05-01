using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;
using Ingress.Data.Interfaces;
using Ingress.Data.Models;

namespace Ingress.WPF.ViewModels.Data
{
    public class ActivityViewModel : INotifyPropertyChanged
    {
        private readonly IActivityRepository _repo;
        private readonly Activity _activity;

        public ICommand SaveCommand => new AsyncCommand(Save);

        public ActivityViewModel(IActivityRepository repo, Activity activity)
        {
            _repo = repo;
            _activity = activity;

            Type = activity.GetType().ToString().Substring(activity.GetType().ToString().LastIndexOf(".") + 1);
        }

        public int ActivityID => _activity.ActivityID;
        public DateTime InsertedAt => _activity.InsertedAt;
        public string Username => _activity.Username;
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

        public string Type { get; }

        private async Task Save()
        {
            if (_activity.ActivityID == 0)
                _repo.Create(_activity);
            else
                _repo.Update(_activity);

            await _repo.SaveChanges();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [JetBrains.Annotations.NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}