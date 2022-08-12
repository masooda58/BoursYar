using Microsoft.AspNetCore.Authorization;
using System;

namespace BoursYar.Authorization.Attribute
{
    /// <summary>
    ///
    /// نگه می دارد Claim ایجاد می کند و مقدار آن را بعنوان یک BoursYarAuthorization به نام Attribut این کلاس برای این است که
    ///<para>
    ///  ها کار کند Method بگونه ای نوشته شده است که فقط روی Attribut این
    /// </para>
    /// </summary>
    //  فقط روی متد کار کند
    [AttributeUsage(AttributeTargets.Method)]
    public class BoursYarAuthorizAttribute : AuthorizeAttribute
    {
        /// <summary>
        ///  را می گیرد و به کلاس پدرارجاع می دهد Policy نام یک
        /// </summary>
        /// <param name="claimToAuthoriz">
        ///  قید می شود Authoriz Auttribute این پارمتر همان پارامتری است که در نام
        /// </param>
        /// <remarks>
        /// </remarks>
        /// <example>
        /// <code>
        /// [BoursYarAuthoriz(ClaimToAuthoriz="Action name")]
        /// </code>
        /// </example>
        //یک ورودی را از سازنده می گیرد و ، یک پالیسی هم به کلاس پدر پاس می دهد
        public BoursYarAuthorizAttribute(string claimToAuthoriz) : base("BoursYarAuthorization")
        {
            ClaimToAuthoriz = claimToAuthoriz;
        }

       
        /// <summary>
        ///  باید مقدار دهی بشود BoursYareAttribute مقداری که در
        /// </summary>
        public string ClaimToAuthoriz { get; }
    }
}
