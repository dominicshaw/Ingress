using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Ingress.WPF.ViewModels.Validation
{
    public class ValidationAttributesByProperty
    {
        public ValidationAttributesByProperty(PropertyInfo property, List<ValidationAttribute> attributes)
        {
            Property = property;
            Attributes = attributes;
        }

        public PropertyInfo Property { get; }
        public List<ValidationAttribute> Attributes { get; }
    }
}