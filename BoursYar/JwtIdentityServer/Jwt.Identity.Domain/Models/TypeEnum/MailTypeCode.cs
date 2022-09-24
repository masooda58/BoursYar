using System.Text.Json.Serialization;

namespace Jwt.Identity.Domain.Models.TypeEnum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MailTypeCode
    {

        MailAccountConfirmationCode,
        MailAccountPasswordResetCode,
        
    
    }
}
