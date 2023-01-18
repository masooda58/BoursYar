using System;
using System.Collections.Generic;

#nullable disable

namespace Admin.UI.MVC.Models
{
    public partial class UserLogInOutLog
    {
        public Guid IdGuid { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public int SignInOut { get; set; }
        public DateTime Time { get; set; }

        public virtual SessionEntity Session { get; set; }
    }
}
