using System.Threading.Tasks;
using Jwt.Identity.Domain.Models;
using Jwt.Identity.Domain.Models.ResultModels;
using Jwt.Identity.Domain.Models.TypeCode;

namespace Jwt.Identity.Domain.Interfaces.IConfirmCode
{
    public interface ITotpCode
    {

        public Task<ConfirmResult> SendTotpCodeAsync(string phoneNo, TotpTypeCode type);

        public Task<ConfirmResult> ConfirmTotpCodeAsync(string phoneNo, string code, TotpTypeCode type);

    }

 
}
