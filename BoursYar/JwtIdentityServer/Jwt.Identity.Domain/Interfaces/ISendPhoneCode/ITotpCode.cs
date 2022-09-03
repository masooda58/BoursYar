using System.Threading.Tasks;
using Jwt.Identity.Domain.Models.ResultModels;

namespace Jwt.Identity.Domain.Interfaces.ISendPhoneCode
{
    public interface ITotpCode
    {

        public Task<PhoneTotpResult> SendTotpCodeAsync(string phoneNo,string sendType);

        public Task<PhoneTotpResult> ConfirmTotpCodeAsync(string phoneNo, string code, string confirmType);

    }

 
}
