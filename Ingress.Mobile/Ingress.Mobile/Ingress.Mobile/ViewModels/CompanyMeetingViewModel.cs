using Ingress.DTOs;

namespace Ingress.Mobile.ViewModels
{
    public sealed class CompanyMeetingViewModel : ActivityBaseViewModel
    {
        private readonly CompanyMeetingDTO _model;

        public bool IsDirect
        {
            get { return _model.IsDirect ?? false; }
            set
            {
                if (value == _model.IsDirect)
                    return;
                _model.IsDirect = value;
                OnPropertyChanged();
            }
        }

        public CompanyMeetingViewModel(CompanyMeetingDTO model) : base(model)
        {
            _model = model;
        }

        protected override bool Validate()
        {
            return true;
        }

        protected override void Commit()
        {
            
        }
    }
}