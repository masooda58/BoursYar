using System;
using System.Collections.Generic;

#nullable disable

namespace Admin.UI.MVC.Models
{
    public partial class Client
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string EmailConfirmPage { get; set; }
        public string EmailResetPage { get; set; }
        public string BaseUrl { get; set; }
        public string LoginUrl { get; set; }
        public string SignInExternal { get; set; }
        public string SignOut { get; set; }
        public string Lockout { get; set; }
        public int LoginType { get; set; }
    }
}
