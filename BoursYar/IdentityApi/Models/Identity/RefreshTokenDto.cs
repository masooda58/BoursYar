using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityApi.Models.Identity
{
    public class RefreshTokenDto
    {
        [Key]
        public Guid IdGuid { get; set; }


        public string UserId { get; set; }
        public string RefreshToken { get; set; }

        [ForeignKey("UserId")]

        public virtual ApplicationUser User { get; set; }
    }
}
