using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Ingress.WPF.ViewModels.Attributes
{
    public sealed class RequiredIfAttribute : ValidationAttribute
    {
        /// <summary>
        /// The name of the property which must equal one of 'Values' for validation to occur.
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// Gets or sets the values which when 'PropertyName' matches one or more of them, validation will occur.
        /// </summary>
        public object[] Values { get; private set; }

        public RequiredIfAttribute(string propertyName, params object[] equalsValues)
        {
            PropertyName = propertyName;
            Values = equalsValues;
        }

        /// <summary>
        /// Performs the conditional validation.
        /// </summary>
        /// <param name="value">This is the actual value of the property who has the RequiredIf attribute on it (e.g. the property that we are validating).</param>
        /// <param name="validationContext">The validation context which contains the instance that owns the member which we are validating.</param>
        /// <returns>Returns ValidationResult.Success upon success, and a ValidationResult with the specified ErrorMessage upon failure.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool isRequired = IsRequired(validationContext);

            if (isRequired && string.IsNullOrEmpty(Convert.ToString(value)))
                return new ValidationResult(ErrorMessage);
            return ValidationResult.Success;
        }

        /// <summary>
        /// Determines whether or not validation should occur.
        /// </summary>
        /// <param name="validationContext">The validation context which contains the instance that owns the member which we are validating.</param>
        /// <returns>Returns true if validation should occur, false otherwise.</returns>
        private bool IsRequired(ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(PropertyName);

            if (property == null) 
                return false;

            var currentValue = property.GetValue(validationContext.ObjectInstance, null);

            foreach (var val in Values)
                if (Equals(currentValue, val))
                    return true;

            return false;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class RequiredIf123Attribute : ValidationAttribute
    {
        #region Properties

        /// <summary>
        /// Gets or sets the other property name that will be used during validation.
        /// </summary>
        /// <value>
        /// The other property name.
        /// </value>
        public string OtherProperty { get; private set; }

        /// <summary>
        /// Gets or sets the display name of the other property.
        /// </summary>
        /// <value>
        /// The display name of the other property.
        /// </value>
        public string OtherPropertyDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the other property value that will be relevant for validation.
        /// </summary>
        /// <value>
        /// The other property value.
        /// </value>
        public object OtherPropertyValue { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether other property's value should match or differ from provided other property's value (default is <c>false</c>).
        /// </summary>
        /// <value>
        ///   <c>true</c> if other property's value validation should be inverted; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// How this works
        /// - true: validated property is required when other property doesn't equal provided value
        /// - false: validated property is required when other property matches provided value
        /// </remarks>
        public bool IsInverted { get; set; }

        /// <summary>
        /// Gets a value that indicates whether the attribute requires validation context.
        /// </summary>
        /// <returns><c>true</c> if the attribute requires validation context; otherwise, <c>false</c>.</returns>
        public override bool RequiresValidationContext
        {
            get { return true; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfAttribute"/> class.
        /// </summary>
        /// <param name="otherProperty">The other property.</param>
        /// <param name="otherPropertyValue">The other property value.</param>
        public RequiredIf123Attribute(string otherProperty, object otherPropertyValue)
            : base("'{0}' is required because '{1}' has a value {3}'{2}'.")
        {
            OtherProperty = otherProperty;
            OtherPropertyValue = otherPropertyValue;
            IsInverted = false;
        }

        #endregion

        /// <summary>
        /// Applies formatting to an error message, based on the data field where the error occurred.
        /// </summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>
        /// An instance of the formatted error message.
        /// </returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                ErrorMessageString,
                name,
                OtherPropertyDisplayName ?? OtherProperty,
                OtherPropertyValue,
                IsInverted ? "other than " : "of ");
        }

        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext == null)
                return new ValidationResult(string.Format(CultureInfo.CurrentCulture, "Could not find validation context.", OtherProperty));
            //{
            //    throw new ArgumentNullException(nameof(validationContext));
            //}

            var otherProperty = validationContext.ObjectType.GetProperty(OtherProperty);
            if (otherProperty == null)
            {
                return new ValidationResult(
                    string.Format(CultureInfo.CurrentCulture, "Could not find a property named '{0}'.", OtherProperty));
            }

            var otherValue = otherProperty.GetValue(validationContext.ObjectInstance);

            // check if this value is actually required and validate it
            if (!IsInverted && Equals(otherValue, OtherPropertyValue) ||
                IsInverted && !Equals(otherValue, OtherPropertyValue))
            {
                if (value == null)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }

                // additional check for strings so they're not empty
                if (value is string val && val.Trim().Length == 0)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }
    }
}
