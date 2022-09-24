using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityPersianHelper.DataAnnotations
{
    public class PhoneOrEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null ) 
            {

                string valueAsString = value.ToString();
                if (string.IsNullOrEmpty(valueAsString))
                {
                    return ValidationResult.Success;
                }

                var emailValidtionAttribute = new EmailAddressAttribute();
                var mobilValidation = new MobileNoAttribute();

                if (emailValidtionAttribute.IsValid(valueAsString) || mobilValidation.IsValid(valueAsString))
                {
                    return ValidationResult.Success;
                }

                return valueAsString.Contains("@")
                    ? new ValidationResult("ایمیل وارد شده معتبر نیست")
                    : new ValidationResult("شماره موبایل وارد شده معتبر نیست");


            }

            return ValidationResult.Success;

        }
    }
}
