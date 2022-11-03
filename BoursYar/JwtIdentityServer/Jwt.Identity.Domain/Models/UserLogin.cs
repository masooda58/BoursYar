using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Identity.Domain.Models
{
   public  class UserLogin
    {
 
        public string UserId { get; set; }

        public string AccessToken { get; set; }

        public string Device { get; set; }

        public string IpAdress { get; set; }

        public DateTime LoginTime { get; set; }=DateTime.Now;

      
    }
}
