using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using System.Threading.Tasks;
using IdentityPersianHelper.DataAnnotations;
using Jwt.Identity.BoursYarServer.Helpers.Extensions;
using Jwt.Identity.Domain.Models;
using Jwt.Identity.Domain.Models.TypeEnum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace Jwt.Identity.BoursYarServer.Models.ViewModel
{
    public class TotpDto
    {
        private string _normalEmailOrPhone;
        [Required]
        [MobileNo]
        public string PhoneNumber    
        {
            get => _normalEmailOrPhone.ToNormalPhoneNo();
            set => _normalEmailOrPhone = value;
        }
        [Required]
      
        public TotpTypeCode TotpType { get; set; }
    }


}
