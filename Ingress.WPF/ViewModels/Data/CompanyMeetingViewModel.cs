using System.ComponentModel.DataAnnotations;
using Ingress.Data.Interfaces;
using Ingress.Data.Models;

namespace Ingress.WPF.ViewModels.Data
{
    public sealed class CompanyMeetingViewModel : ActivityViewModel
    {
        private readonly CompanyMeeting _activity;

        public CompanyMeetingViewModel(IActivityRepository repo, CompanyMeeting activity) : base(repo, activity)
        {
            _activity = activity;
        }

        public string GlobalID => _activity.GlobalID;
        public string ConvoID => _activity.ConvoID;
        public string Organiser => _activity.Organiser;
        public string Categories => _activity.Categories;
        
        [Required(ErrorMessage = "You must specify whether this company meeting was via a broker or direct.")]
        public bool? IsDirect
        {
            get => _activity.IsDirect;
            set
            {
                if (value == _activity.IsDirect) return;
                _activity.IsDirect = value;
                OnPropertyChanged();
            }
        }
    }
}