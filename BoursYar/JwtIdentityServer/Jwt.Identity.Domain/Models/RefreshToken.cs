using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jwt.Identity.Domain.Models
{
    public class RefreshToken
    {
        [Key]
        public Guid IdGuid { get; set; }

   
        public string UserId { get; set; }
        public string TempRefreshToken { get; set; }

        [ForeignKey("UserId")]

        public virtual ApplicationUser User { get; set; }
    }
}
