using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jwt.Identity.Domain.Models
{
    public class RefreshToken
    {
        [Key]
        public Guid IdGuid { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public string TempRefreshToken { get; set; }
        public DateTime CreatTime
        {
            get;
            set;
        }

        public virtual ApplicationUser User { get; set; }
    }
}
