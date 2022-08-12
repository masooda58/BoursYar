using BoursYar.Authorization.repositories;
using BoursYar.Authorization.Requirement;
using BoursYar.Authorization.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features.Authentication;

namespace BoursYar.Authorization.Handler
{
    /// <summary>
    /// تعیین می کندایا این مسیر دسترسی دارد یا خیر
    /// </summary>
    public class ClaimBaseHandler : AuthorizationHandler<ClaimBaseRequirement>
    {
        //private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IClaimBaseAuthorizationUtilities _utilities;
        private readonly IHttpContextAccessor _contextAccessor;
        //private readonly SignInContext _signInManager;

        /// <summary>
        ///  دو پارامتر مورد نیاز را مقدار دهی می کند
        /// </summary>
        /// <param name="utilities">   به این لینک مراجه کنید <see cref="IClaimBaseAuthorizationUtilities"/></param>
        /// <param name="contextAccessor">Context route دریافت اطلاعات از</param>
        public ClaimBaseHandler(IClaimBaseAuthorizationUtilities utilities, IHttpContextAccessor contextAccessor)
        {
            // _signInManager = signInManager;
            _utilities = utilities;
            _contextAccessor = contextAccessor;
        }

        /// <summary>
        ///  Authoriz policy برای handel
        /// </summary>
        /// <param name="context">AuthorizationHandlerContext</param>
        /// <param name="requirement"> به رجوع کنید <see cref="ClaimBaseRequirement"/></param>
        /// <returns>
        ///  Claim پذیرش یا رد
        /// </returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimBaseRequirement requirement)
        {
            // باید کاربر داشته باشد Claim درخواستی کاربر تعیین می کنند چه Route با استفاده از
            var claimToAuthoriz = _utilities.GetClaimToAuthorize(_contextAccessor.HttpContext);
            // را نداشت [ClaimBaseAttribut] درخواستی کاربر Rout اگر
            if (string.IsNullOrWhiteSpace(claimToAuthoriz))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            //if (!_signInManager.IsSignedIn(context.User))
            //{
            //    return Task.CompletedTask;
            //}
            // وجود داشت user های Claim درخواستی کاربر در لیست Rout موجود در Claim اگر
            if (context.User.HasClaim(ClaimStore.BoursYarAccess, claimToAuthoriz))
            {

                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
