using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;

namespace Ingress.WPF.ViewModels.Validation
{
    public abstract class ValidationViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly ValidationContext _validationContext;
        private readonly Dictionary<string, ValidationAttributesByProperty> _properties;

        protected ValidationViewModel()
        {
            _validationContext = new ValidationContext(this, null, null);
            _properties = ValidationAttributes.TryGet(GetType()).GetPropertiesAsDictionary();
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                if (!_properties.TryGetValue(columnName, out var prop)) 
                    return null;

                var err = string.Empty;

                foreach(var att in prop.Attributes)
                    if (att.GetValidationResult(prop.Property.GetValue(this), _validationContext) != null)
                        err = err + att.ErrorMessage + "\n";

                if (err == string.Empty)
                    return null;

                return err.TrimEnd();
            }
        }

        public string Error
        {
            get
            {
                var errors = new StringBuilder();

                foreach (var prop in _properties.Values)
                {
                    foreach (var att in prop.Attributes)
                    {
                        var result = att.GetValidationResult(prop.Property.GetValue(this), _validationContext);

                        if (result != null)
                            errors.AppendLine(att.ErrorMessage);
                    }
                }

                return errors.ToString().Trim();
            }
        }

        public bool IsValid
        {
            get
            {
                foreach (var prop in _properties.Values)
                {
                    foreach (var att in prop.Attributes)
                    {
                        var result = att.GetValidationResult(prop.Property.GetValue(this), _validationContext);
                        
                        if (result != null)
                            return false;
                    }
                }

                return true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}