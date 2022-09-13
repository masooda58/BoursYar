using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jwt.Identity.BoursYarServer.Models.ViewModel;
using Jwt.Identity.Domain.Interfaces.IConfirmCode;
using Jwt.Identity.Domain.Models.ResultModels;
using Jwt.Identity.Domain.Models.TypeCode;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace Jwt.Identity.BoursYarServer.Controllers
{
    //[Route("totp/[controller]")]
    //[ApiController]
    public class TotpApi : ControllerBase
    {
        private readonly ITotpCode _TotpService;

        public TotpApi(ITotpCode totpService)
        {
            _TotpService = totpService;
        }

        [HttpPost]
        [Route("SendTotp")]
        public async Task<JsonResult> SendTotp([FromForm]TotpDto totpDto)
        {
        
            var result = await _TotpService.SendTotpCodeAsync(totpDto.PhoneNumber, totpDto.TotpType);
            return new JsonResult(result);
        }
        [HttpPost]
        [Route("ConfirmTotp")]
        public async Task<ConfirmResult> ConfirmTotp(string phoneNo,[FromForm]string code)
        {
           
            
            var result = await _TotpService.ConfirmTotpCodeAsync(phoneNo, code,TotpTypeCode.TotpAccountPasswordResetCode);
            return result;
            //return new PartialViewResult()
            //{
            //    ViewName = "\\Areas\\Account\\pages\\Shared\\_TotpCofirmationPrtial.cshtml",
            //    ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            //    {
            //        Model = new TotpDto() {   
            //            TotpType = TotpTypeCode.TotpAccountPasswordResetCode,
            //            PhoneNumber = "989123406286"

            //        },


            //    }

        //};
        }
    }
}
//[HttpPost]
//[Route("ConfirmTotp")]
//public async Task<JsonResult> ConfirmTotp([FromForm]TotpDto totpDto,[FromForm]string code)
//{
           
            
//    var result = await _TotpService.ConfirmTotpCodeAsync(totpDto.PhoneNumber, code,totpDto.TotpType);
//    return new JsonResult(result);
//}
