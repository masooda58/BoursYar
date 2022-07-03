using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IdentityApi.Models.Identity;

namespace IdentityApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        public bool ApplicationEditingAllowed { get; set; } = true;
        public bool Approved { get; set; } = false;

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [NotMapped]
        public string FullName => FirstName + " " + LastName;
        
        public virtual RefreshTokenDto RefreshTokenDto { get; set; }
    }
}
