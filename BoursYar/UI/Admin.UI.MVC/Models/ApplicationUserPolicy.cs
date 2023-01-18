using System;
using System.Collections.Generic;

#nullable disable

namespace Admin.UI.MVC.Models
{
    public partial class ApplicationUserPolicy
    {
        public string PolicyId { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUser User { get; set; }
        public virtual UserLoginPolicyOption UserLoginPolicyOption { get; set; }
    }
}
