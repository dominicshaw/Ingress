using System.ComponentModel.DataAnnotations;
using Ingress.Data.Models;

namespace Ingress.WPF.ViewModels.Data
{
    public sealed class ModelAccessViewModel : ActivityViewModel
    {
        private readonly ModelAccess _activity;

        public ModelAccessViewModel(ModelAccess activity) : base(activity)
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
        
        public override string Type => "Model Access";
        public override Activity GetModel() => _activity;
    }
}