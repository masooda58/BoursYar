using System.ComponentModel.DataAnnotations;

namespace PersianTools.Core.CustomValidation
{
   public class MobileNoAttribute : ValidationAttribute
    {
      
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.ToString().IsMobileNoValid())
                return ValidationResult.Success;
            return new ValidationResult("شماره موبایل وارد شده معتبر نیست");
        }
    }
}
