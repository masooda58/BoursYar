using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityPersianHelper.DataAnnotations;
using Jwt.Identity.Domain.Models.TypeEnum;

namespace Jwt.Identity.Domain.Models.Request
{
    public class TotpConfirmationCodeRequest
    {
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [Display(Name = "کد ارسالی")]
        public string Code { get; set; }
        [MobileNo]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [Display(Name = "شماره موبایل")]
        public string? PhoneNo { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [Display(Name = "نوع ")]
        public string? TotpType { get; set; }
    }
}
