using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace PersianTranslation.DataAnnotations
{
    class PhoneOrEmailAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string valueAsString = value.ToString();

            const string emailRegex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            bool isValidEmail = Regex.IsMatch(valueAsString, emailRegex);

            if (isValidEmail)
            {
                return ValidationResult.Success;
            }

            // const string usaPhoneNumbersRegex = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";
            const string mobPattern = @"^((\+9|\+989|\+\+989|9|09|989|0989|00989)(01|02|03|10|11|12|13|14|15|16|17|18|19|20|21|22|30|31|32|33|34|35|36|37|38|39|90))(\d{7})$";
            bool isValidPhone = Regex.IsMatch(valueAsString, mobPattern);

            if (isValidPhone)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("ایمیل یا شماره موبایل معتبر نیست");
        }
    }
}
