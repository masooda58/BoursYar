using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoursYar.Authorization.Utilities;
using BoursYar.Authorization.Utilities.MvcNameUtilities;

namespace BoursYar.Authorization.HelpFile
{
    /// <summary>
    /// راهنمای کاربر
    /// </summary>
    /// <remarks>
    ///  Stratup.cs  گام اول اضافه کردن کد زیر به فایل
    /// <code>
    ///   services.AddBoursYarAuthorize();
    /// </code>
    /// دارند Authorize که نیاز به Action زیر برای Attribute گام دوم اضافه کردن
    /// <code>
    /// [BoursYarAuthoriz=(ClaimToAuthoriz="Action Name")]
    /// </code>
    /// <para>
    ///  <see cref="IClaimBaseAuthorizationUtilities"/>
    /// داردClaim که کاربر درخواست کرده چه Rout
    /// <code>
    /// [BoursYarAuthoriz=(ClaimToAuthoriz="Action Name")]
    /// public ActionREsault test(){....}
    /// 
    ///  var c= new IClaimBaseAuthorizationUtilities();
    ///   var claim= c.GetClaimToAuthorize(HttpContext)="Action Name";
    /// </code>
    /// </para>
    /// <para>
    /// <see cref="IMvcUtilities"/>
    /// را دارندAttribute BoursYarAuthoriz دو متغیر دارد یکی برای همه اجزا و دیگری اجزای که
    /// <code>
    /// var f=new IMvcUtilities();
    /// f.Mvcifo="MVC همه";
    /// f.ActionThatRequireClaimBaseAuthorazition //فقط اتربیت دارها
    /// </code>
    /// </para>
    /// 
    /// </remarks>
   public interface IUserGuide
    {
    }
}
