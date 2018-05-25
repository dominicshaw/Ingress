using System;
using Ingress.DTOs;
using Ingress.Mobile.Helpers;

namespace Ingress.Mobile.ViewModels
{
    public sealed class AnalystMeetingViewModel : ActivityBaseViewModel
    {
        private readonly AnalystMeetingDTO _model;

        private int? _hours;
        private int? _minutes;

        public int? Hours
        {
            get => _hours;
            set
            {
                if (value == _hours) return;
                _hours = value;
                OnPropertyChanged();
            }
        }
        public int? Minutes
        {
            get => _minutes;
            set
            {
                if (value == _minutes) return;
                _minutes = value;
                OnPropertyChanged();
            }
        }
        public string Analyst
        {
            get => _model.Analyst;
            set
            {
                if (value == _model.Analyst) return;
                _model.Analyst = value;
                OnPropertyChanged();
            }
        }
        public bool IsConference
        {
            get => _model.IsConference ?? false;
            set
            {
                if (value == _model.IsConference) return;
                _model.IsConference = value;
                OnPropertyChanged();
            }
        }

        public AnalystMeetingViewModel(AnalystMeetingDTO model) : base(model)
        {
            _model = model;

            if (model.ActivityID == 0)
                _model.TimeTaken = (_model.DateEnd - _model.DateStart).ToString();

            if (!string.IsNullOrWhiteSpace(_model.TimeTaken) && TimeSpan.TryParse(_model.TimeTaken, out var span))
            {
                _hours = span.Hours;
                _minutes = span.Minutes;

                while (!DataCollections.Minutes.Contains(_minutes.Value))
                {
                    if (_minutes == 0)
                        break;

                    _minutes = _minutes.Value - 1;
                }
            }

            if (!_model.IsConference.HasValue)
                _model.IsConference = false;
        }

        protected override bool Validate()
        {
            if (!_hours.HasValue || !_minutes.HasValue)
            {
                ValidationErrors = "You must select a time-taken range for the meeting.";
                return false;
            }

            if (_hours.Value == 0 && _minutes.Value == 0)
            {
                ValidationErrors = "You must select a time-taken range for the meeting.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Analyst))
            {
                ValidationErrors = "You must enter the name of an analyst.";
                return false;
            }

            return true;
        }

        protected override void Commit()
        {
            _model.TimeTaken = new TimeSpan(_hours.Value, _minutes.Value, 0).ToString();
        }
    }
}