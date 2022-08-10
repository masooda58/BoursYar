using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Jwt.Authentication;
using Jwt.Identity.BoursYarServer.Services.TokenServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#pragma warning disable CS1591 // همه توابع راهنما ندارند

namespace Jwt.Identity.Test.Helper
{
    /// <summary>
    ///  API برای پروژه Helper
    /// </summary>
    public static class ApiHelper
    {
        public static TokenGenrators CreaTokenGenrators()
        {
            return new TokenGenrators(LoadJson());

        }
        public static TokenValidators CreaTokenValidators()
        {
            return new TokenValidators(LoadJson());

        }
        private static JwtSettingModel LoadJson()
        {
            using (StreamReader r = new StreamReader("JwtIdentitySharedSettings.json"))
            {
                string json = r.ReadToEnd();
                var data = (JObject)JsonConvert.DeserializeObject(json);
                var jwt =data["JWT"].ToString();
                JwtSettingModel items = JsonConvert.DeserializeObject<JwtSettingModel>(jwt);
                return items;
            }
        }
        
    }
}
