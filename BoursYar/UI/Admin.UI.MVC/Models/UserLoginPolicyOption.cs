using System;
using System.Collections.Generic;

#nullable disable

namespace Admin.UI.MVC.Models
{
    public partial class UserLoginPolicyOption
    {
        public string Id { get; set; }
        public string PolicyName { get; set; }
        public int NumberOfLogin { get; set; }
        public int OvereNumberOfLogin { get; set; }

        public virtual ApplicationUserPolicy IdNavigation { get; set; }
    }
}
