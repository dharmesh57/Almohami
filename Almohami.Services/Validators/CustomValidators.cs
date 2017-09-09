using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Services.Validators
{
    //// <summary>
    // Date Compare Validator
    // <//summary>
    public class DateCompareValidator : ValidationAttribute
    {
        // <summary>
        // Initializes a new instance of the <see cref="DateCompareValidator"//> class.
        // <//summary>
        // <param name="otherPropertyName">Name of the other property.<//param>
        // <param name="erroMessage">The error message.<//param>
        public DateCompareValidator(string otherPropertyName, string erroMessage)
            : base(erroMessage)
        {
            OtherPropertyName = otherPropertyName;
        }

        // <summary>
        // Gets or sets the name of the other property.
        // <//summary>
        // <value>
        // The name of the other property.
        // <//value>
        public string OtherPropertyName { get; set; }

        // <summary>
        // Validates the specified value with respect to the current validation attribute.
        // <//summary>
        // <param name="value">The value to validate.<//param>
        // <param name="validationContext">The context information about the validation operation.<//param>
        // <returns>
        // An instance of the <see cref="T:S:System.ComponentModel.DataAnnotations.ValidationResult" //> class.
        // <//returns>
        protected override ValidationResult IsValid(Object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherPropertyName);
                var otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
                
                if (otherValue == null)
                {
                    var message = FormatErrorMessage(string.Format("Please enter {0}", OtherPropertyName));
                    return new ValidationResult(message);
                }

                var startDate = (DateTime)otherValue;
                var endDate = (DateTime)value;

                if (startDate != DateTime.MinValue)
                {
                    var success = true;
                    if (startDate > endDate)
                    {
                        success = false;
                    }

                    if (!success)
                    {
                        var message = FormatErrorMessage(validationContext.DisplayName);
                        return new ValidationResult(message);
                    }
                }
            }
            return null;
        }
    }

    /// <summary>
    /// Amount CompareValidator
    /// </summary>
    public class AmountCompareValidator : ValidationAttribute
    {
        // <summary>
        // Initializes a new instance of the <see cref="DateCompareValidator"//> class.
        // <//summary>
        // <param name="otherPropertyName">Name of the other property.<//param>
        // <param name="erroMessage">The error message.<//param>
        public AmountCompareValidator(string otherPropertyName, string erroMessage)
            : base(erroMessage)
        {
            OtherPropertyName = otherPropertyName;
        }

        // <summary>
        // Gets or sets the name of the other property.
        // <//summary>
        // <value>
        // The name of the other property.
        // <//value>
        public string OtherPropertyName { get; set; }

        // <summary>
        // Validates the specified value with respect to the current validation attribute.
        // <//summary>
        // <param name="value">The value to validate.<//param>
        // <param name="validationContext">The context information about the validation operation.<//param>
        // <returns>
        // An instance of the <see cref="T:S:System.ComponentModel.DataAnnotations.ValidationResult" //> class.
        // <//returns>
        protected override ValidationResult IsValid(Object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherPropertyName);
                var otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
                var contractualAmount = (long)otherValue;
                var availableAmount = (long)value;

                if (contractualAmount > 0)
                {
                    var success = true;
                    if (availableAmount > contractualAmount)
                    {
                        success = false;
                    }

                    if (!success)
                    {
                        var message = FormatErrorMessage(validationContext.DisplayName);
                        return new ValidationResult(message);
                    }
                }
            }
            return null;
        }
    }
}
