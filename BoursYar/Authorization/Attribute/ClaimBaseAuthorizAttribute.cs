using System;
using Microsoft.AspNetCore.Authorization;

namespace BoursYarAuthorization.Attribute
{
    // این اتریبیوت برای این است که فقط روی متد کار کند
    [AttributeUsage(AttributeTargets.Method)]
    public class ClaimBaseAuthorizAttribute:AuthorizeAttribute
    {
        public ClaimBaseAuthorizAttribute(string claimToAuthoriz):base("ClaimBaseAuthorization")
        {
            ClaimToAuthoriz = claimToAuthoriz;
        }

        //یک ورودی را از سازنده می گیرد و ، یک پالیسی هم به کلاس پدر پاس می دهد
        public string ClaimToAuthoriz  { get; }
    }
}
