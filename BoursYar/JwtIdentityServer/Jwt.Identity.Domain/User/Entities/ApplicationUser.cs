﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Jwt.Identity.Domain.User.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)] public string FirstName { get; set; }

        [MaxLength(100)] public string LastName { get; set; }

        public bool Approved { get; set; } = false;

        [NotMapped] public string FullName => FirstName + " " + LastName;

        public virtual UserLogInOutLog UserLogInOutLoger { get; set; }
    }
}