using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Jwt.Identity.Domain.Models.TypeEnum
{
  
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LoginType
    {

        Token,
        Cookie,
        TokenAndCookie
        
    
    }
}
