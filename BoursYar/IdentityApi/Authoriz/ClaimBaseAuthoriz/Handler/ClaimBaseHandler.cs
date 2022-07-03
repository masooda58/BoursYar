using System.Threading.Tasks;
using IdentityApi.Authoriz.ClaimBaseAuthoriz.Requirement;
using IdentityApi.Authoriz.ClaimBaseAuthoriz.Utilities;
using IdentityApi.Models;
using IdentityApi.repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Authoriz.ClaimBaseAuthoriz.Handler
{
    public class ClaimBaseHandler:AuthorizationHandler<ClaimBaseRequirement>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IClaimBaseAuthorizationUtilities _utilities;
        private readonly IHttpContextAccessor _contextAccessor;

        public ClaimBaseHandler(SignInManager<ApplicationUser> signInManager, IClaimBaseAuthorizationUtilities utilities, IHttpContextAccessor contextAccessor)
        {
            _signInManager = signInManager;
            _utilities = utilities;
            _contextAccessor = contextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimBaseRequirement requirement)
        {
            var claimToAuthoriz = _utilities.GetClaimToAuthorize(_contextAccessor.HttpContext);// methodi ke check mikon aya yek action atrribute claimbaseauthoriz ra darad ya khire
            if (string.IsNullOrWhiteSpace(claimToAuthoriz))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (!_signInManager.IsSignedIn(context.User))
            {
                return Task.CompletedTask;
            }

            if (context.User.HasClaim(ClaimStore.UserAccess, claimToAuthoriz))
            {
            context.Succeed(requirement);
            return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
