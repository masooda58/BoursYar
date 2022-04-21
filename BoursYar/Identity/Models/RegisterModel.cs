using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity
{
   public class RegisterModel
    {
        [Required(ErrorMessage = "نام را وارد کنید")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "ایمیل صحیح را وارد کنید")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        [Compare(nameof(Password),ErrorMessage = "تکرار کلمه عبور صحیح نیست")]
        public string? ConfirmPassword { get; set; }

    }
}
