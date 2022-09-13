using System;
using System.Collections.Generic;
using System.Linq;

using System.Text.Json.Serialization;


namespace Jwt.Identity.Domain.Models.TypeCode
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MailTypeCode
    {

        MailAccountConfirmationCode,
        MailAccountPasswordResetCode,
        
    
    }
}
