using System;
using System.ComponentModel.DataAnnotations;
using Ingress.Data.Interfaces;
using Ingress.Data.Models;

namespace Ingress.WPF.ViewModels.Data
{
    public sealed class ModelAccessViewModel : ActivityViewModel
    {
        private readonly ModelAccess _activity;

        public ModelAccessViewModel(IActivityRepository repo, ModelAccess activity) : base(repo, activity)
        {
            _activity = activity;
        }
        
        [Required(ErrorMessage = "You must enter an analyst for this model.")]
        public string Analyst
        {
            get => _activity.Analyst;
            set
            {
                if (value == _activity.Analyst) return;
                _activity.Analyst = value;
                OnPropertyChanged();
            }
        }
    }
}