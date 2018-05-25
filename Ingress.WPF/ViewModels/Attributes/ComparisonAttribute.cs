using System;
using System.ComponentModel.DataAnnotations;

namespace Ingress.WPF.ViewModels.Attributes
{
    public abstract class ComparisonAttribute : ValidationAttribute
    {
        private readonly string _otherProperty;

        protected ComparisonAttribute(string otherProperty, string errorPattern)
            : base(errorPattern)
        {
            _otherProperty = otherProperty;
        }

        private string SetErrorMessage(string err)
        {
            ErrorMessage = err;
            return err;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, _otherProperty);
        }

        protected override ValidationResult IsValid(object firstValue, ValidationContext validationContext)
        {
            if (validationContext?.ObjectInstance == null)
                return new ValidationResult(SetErrorMessage("Enter a number."));
            return IsValid(firstValue, validationContext.ObjectType, validationContext.ObjectInstance,
                validationContext.DisplayName);
        }

        private ValidationResult IsValid(object firstValue, Type objectType, object objectInstance, string displayName)
        {
            var a = firstValue as IComparable;
            var secondComparable = GetSecondComparable(objectType, objectInstance);

            if (a == null || secondComparable == null || Compare(a, secondComparable))
                return ValidationResult.Success;

            return new ValidationResult(SetErrorMessage(FormatErrorMessage(displayName ?? "Field")), new[] { displayName ?? "Field" });
        }

        protected abstract bool Compare(IComparable a, IComparable b);

        private IComparable GetSecondComparable(Type t, object viewModel)
        {
            var property = t.GetProperty(_otherProperty);

            if (property != null)
                return property.GetValue(viewModel, null) as IComparable;

            return null;
        }
    }
}