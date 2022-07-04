using System.Threading.Tasks;
using BoursYar.Authorization.repositories;
using BoursYar.Authorization.Requirement;
using BoursYar.Authorization.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace BoursYar.Authorization.Handler
{
    public class ClaimBaseHandler:AuthorizationHandler<ClaimBaseRequirement>
    {
        //private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IClaimBaseAuthorizationUtilities _utilities;
        private readonly IHttpContextAccessor _contextAccessor;

        public ClaimBaseHandler( IClaimBaseAuthorizationUtilities utilities, IHttpContextAccessor contextAccessor)
        {
           // _signInManager = signInManager;
            _utilities = utilities;
            _contextAccessor = contextAccessor;
        }

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
