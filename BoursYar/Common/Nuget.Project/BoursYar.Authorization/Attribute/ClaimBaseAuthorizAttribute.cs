using Microsoft.AspNetCore.Authorization;
using System;

namespace BoursYar.Authorization.Attribute
{
    // این اتریبیوت برای این است که فقط روی متد کار کند
    [AttributeUsage(AttributeTargets.Method)]
    public class BoursYarAuthorizAttribute : AuthorizeAttribute
    {
        public BoursYarAuthorizAttribute(string claimToAuthoriz) : base("BoursYarAuthorization")
        {
            ClaimToAuthoriz = claimToAuthoriz;
        }

        //یک ورودی را از سازنده می گیرد و ، یک پالیسی هم به کلاس پدر پاس می دهد
        public string ClaimToAuthoriz { get; }
    }
}
