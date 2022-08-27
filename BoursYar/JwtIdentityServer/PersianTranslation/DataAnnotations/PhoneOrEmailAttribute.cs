using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace PersianTranslation.DataAnnotations
{
  public  class PhoneOrEmailAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string valueAsString = value.ToString();

          
           var emailValidtionAttribute = new EmailAddressAttribute();
           var mobilValidation = new MobileNoAttribute();

            if (emailValidtionAttribute.IsValid(valueAsString)||mobilValidation.IsValid(valueAsString))
            {
                return ValidationResult.Success;
            }

            return valueAsString.Contains("@") ?
                new ValidationResult("ایمیل وارد شده معتبر نیست") 
                : new ValidationResult("شماره موبایل وارد شده معتبر نیست");
        }
    }
}
