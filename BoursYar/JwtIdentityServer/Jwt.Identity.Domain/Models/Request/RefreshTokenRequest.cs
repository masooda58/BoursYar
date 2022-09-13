using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Identity.Domain.Models.Request
{
    public class RefreshTokenRequest
    {
        public string Refreshtoken { get; set; }
    }
}
