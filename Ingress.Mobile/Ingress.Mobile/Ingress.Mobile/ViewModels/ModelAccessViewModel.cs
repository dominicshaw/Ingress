﻿using Ingress.DTOs;

namespace Ingress.Mobile.ViewModels
{
    public class ModelAccessViewModel : ActivityBaseViewModel
    {
        private readonly ModelAccessDTO _model;

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

        public ModelAccessViewModel(ModelAccessDTO model) : base(model)
        {
            _model = model;
        }

        protected override bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Analyst))
            {
                ValidationErrors = "You must enter the name of an analyst.";
                return false;
            }

            return true;
        }

        protected override void Commit()
        {
            
        }
    }
}