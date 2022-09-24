



using System.Text.Json.Serialization;

namespace Jwt.Identity.Domain.Models.TypeEnum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TotpTypeCode
    {

        TotpAccountConfirmationCode,
        TotpAccountPasswordResetCode,
        
    
    }
}