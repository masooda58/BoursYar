using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Models.Requests
{
    public class RefreshTokenRequest
    {
        public string Refreshtoken { get; set; }
    }
}
