



using System.Text.Json.Serialization;

namespace Jwt.Identity.Domain.Models.TypeCode
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TotpTypeCode
    {

        TotpAccountConfirmationCode,
        TotpAccountPasswordResetCode,
        
    
    }
}