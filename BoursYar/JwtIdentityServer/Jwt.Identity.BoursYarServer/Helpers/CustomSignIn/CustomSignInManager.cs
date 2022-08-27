﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jwt.Identity.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Jwt.Identity.BoursYarServer.Helpers.CustomSignIn
{
    public class CustomSignInManager:SignInManager<ApplicationUser>
    {
        public CustomSignInManager(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<ApplicationUser>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<ApplicationUser> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }
        public override async Task<bool> CanSignInAsync(ApplicationUser user)
        {
            var emailConfirmed = await UserManager.IsEmailConfirmedAsync(user);
            var phoneConfirmed = await UserManager.IsPhoneNumberConfirmedAsync(user);
        
            return emailConfirmed || phoneConfirmed;

        }
    }
}
