using System;
using System.Collections.Generic;

#nullable disable

namespace Admin.UI.MVC.Models
{
    public partial class SessionEntity
    {
        public SessionEntity()
        {
            UserLogInOutLogs = new HashSet<UserLogInOutLog>();
        }

        public string SessionId { get; set; }
        public string IpAddress { get; set; }
        public string DeviceName { get; set; }
        public string BrowserName { get; set; }

        public virtual ICollection<UserLogInOutLog> UserLogInOutLogs { get; set; }
    }
}
