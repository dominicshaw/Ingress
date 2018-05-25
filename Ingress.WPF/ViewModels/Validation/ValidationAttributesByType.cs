using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ingress.WPF.ViewModels.Validation
{
    public class ValidationAttributesByType
    {
        private readonly Dictionary<string, ValidationAttributesByProperty> _properties = new Dictionary<string, ValidationAttributesByProperty>();

        public ValidationAttributesByType(Type type)
        {
            foreach (var prop in type.GetProperties())
            {
                foreach (ValidationAttribute att in prop.GetCustomAttributes(typeof(ValidationAttribute), true))
                {
                    if (_properties.TryGetValue(prop.Name, out var details))
                    {
                        details.Attributes.Add(att);
                    }
                    else
                    {
                        details = new ValidationAttributesByProperty(prop, new List<ValidationAttribute> {att});
                        _properties.Add(prop.Name, details);
                    }
                }
            }
        }

        public List<ValidationAttributesByProperty> GetPropertiesAsList()
        {
            return _properties.Values.ToList();
        }

        public Dictionary<string, ValidationAttributesByProperty> GetPropertiesAsDictionary()
        {
            return _properties;
        }
    }
}