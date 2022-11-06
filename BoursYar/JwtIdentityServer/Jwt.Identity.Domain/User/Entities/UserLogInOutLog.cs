﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Jwt.Identity.Domain.User.Enum;

namespace Jwt.Identity.Domain.User.Entities
{
    public class UserLogInOutLog
    {
        [Key] public Guid IdGuid { get; set; }

        [ForeignKey("Id")] public string UserId { get; set; }

        public string Device { get; set; }
        public string IpAdress { get; set; }
        public SignInLogerType SignInOut { get; set; }
        public DateTime Time { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}