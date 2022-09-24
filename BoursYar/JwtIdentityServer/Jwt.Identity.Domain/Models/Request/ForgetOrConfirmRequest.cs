using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityPersianHelper.DataAnnotations;
using IdentityPersianHelper.Extensions;

namespace Jwt.Identity.Domain.Models.Request
{
   public class ForgetOrConfirmRequest
    {
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        // [EmailAddress(ErrorMessage = "{0} وارد شده صحیح نمی باشد")]
        [PhoneOrEmail]
        [Display(Name = "ایمیل یا شماره موبایل")]
        // ErrorMessage = "ایمیل یا شماره موبایل  قبلا  در سایت ثبت نام کرده است",
        public string EmailOrPhone
        {
            get=>_normalEmailOrPhone.ToNormalPhoneNo();
            set=>_normalEmailOrPhone=value;
        }
        private string _normalEmailOrPhone;


    }
}
