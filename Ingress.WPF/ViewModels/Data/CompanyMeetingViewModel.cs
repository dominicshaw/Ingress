using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using DevExpress.Mvvm;
using Ingress.Data.Models;

namespace Ingress.WPF.ViewModels.Data
{
    public sealed class CompanyMeetingViewModel : ActivityViewModel
    {
        private readonly CompanyMeeting _activity;

        public CompanyMeetingViewModel(CompanyMeeting activity) : base(activity)
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
                if (_activity.IsDirect == null && value == false) // super annoying hack because when you click on a togglebutton which is currently null, it moves it to unchecked, which is a really weird behavior if you are clicking a button
                    value = true;
                
                _activity.IsDirect = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(IsBroker));
            }
        }

        public bool? IsBroker
        {
            get => !_activity.IsDirect;
            set
            {
                if (_activity.IsDirect == null && value == false) // super annoying hack because when you click on a togglebutton which is currently null, it moves it to unchecked, which is a really weird behavior if you are clicking a button
                    value = true;

                _activity.IsDirect = !value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(IsDirect));
            }
        }

        public override bool Skipped
        {
            get => _activity.Skipped;
            set
            {
                if (value == _activity.Skipped) return;
                _activity.Skipped = value;
                OnPropertyChanged();
            }
        }
        
        public override string Type => "Company Meeting";
        
        public override Activity GetModel() => _activity;
    }
}