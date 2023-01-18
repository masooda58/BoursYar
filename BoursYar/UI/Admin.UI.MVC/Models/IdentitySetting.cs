using System;
using System.Collections.Generic;

#nullable disable

namespace Admin.UI.MVC.Models
{
    public partial class IdentitySetting
    {
        public string Id { get; set; }
        public bool RequireDigit { get; set; }
        public int RequiredLength { get; set; }
        public bool RequireNonAlphanumeric { get; set; }
        public bool RequireUppercase { get; set; }
        public bool RequireLowercase { get; set; }
        public int RequiredUniqueChars { get; set; }
        public int DefaultLockoutTimeSpanMinute { get; set; }
        public int MaxFailedAccessAttempts { get; set; }
        public bool RequireConfirmedAccount { get; set; }
        public int TokenLifespanHour { get; set; }
        public int TotpLifeSpanMinute { get; set; }
        public int CaptchStrategy { get; set; }
    }
}
