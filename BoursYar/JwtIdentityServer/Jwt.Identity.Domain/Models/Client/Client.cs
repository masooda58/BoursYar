using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jwt.Identity.Domain.Models.TypeEnum;

namespace Jwt.Identity.Domain.Models.Client
{
  public  class Client
    {
  
        [Key] 
        [Required]
        
        public int ClientId { get; set; }

        private string _clientName;
        public string ClientName { get=>_clientName; set=>_clientName=value.ToUpper(); }
        private string _emailConfirmPage;

        public string EmailConfirmPage
        {
            get => _emailConfirmPage;
            set=>_emailConfirmPage=BaseUrl+value;
        }
        private string _emailResetPage;

        public string EmailResetPage
        {
            get =>_emailResetPage;
            set=>_emailResetPage=BaseUrl+value;
        }
        
        public string BaseUrl { get; set; }

        private string _loginUrl;
        public string LoginUrl
        {
            get =>_loginUrl;
            set=>_loginUrl=BaseUrl+value;
        }

        private string _signInExternal;

        public string SignInExternal
        {
            get =>_signInExternal;
            set=>_signInExternal=BaseUrl+value;
        }
        private string _signOut;

        public string SignOut
        {
            get =>_signOut;
            set=>_signOut=BaseUrl+value;
        }
        private string _lockout;
        public string Lockout
        {
            get =>_lockout;
            set=>_lockout=BaseUrl+value;
        }
        public LoginType  LoginType { get; set; }




    }
}
