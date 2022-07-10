using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jwt.Identity.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        public bool Approved { get; set; } = false;
        [NotMapped]
        public string FullName => FirstName + " " + LastName;
        public virtual RefreshToken RefreshToken { get; set; }
    }
}
