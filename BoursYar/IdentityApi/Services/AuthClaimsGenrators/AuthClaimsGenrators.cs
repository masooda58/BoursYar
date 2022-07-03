﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityApi.Models;
using IdentityApi.repositories;
using IdentityApi.Services.UserManagementService;
using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Services.AuthClaimsGenrators
{
    public class AuthClaimsGenrators:IAuthClaimsGenrators
    {
        private readonly IUserManagementService _userManagementService;
        private readonly UserManager<ApplicationUser> _usermanager;


        public AuthClaimsGenrators(IUserManagementService userManagementService, UserManager<ApplicationUser> usermanager)
        {
            _userManagementService = userManagementService;
            _usermanager = usermanager;
        }

        public async Task<List<Claim>> CreatClaims(ApplicationUser user)
        {
            var userRoles = await _userManagementService.GetUserRoleAsync(user);
            var userClaimes=await _usermanager.GetClaimsAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimStore.UserAccess,"x"),
                new Claim(ClaimStore.UserAccess,"y"),
                new Claim("id",user.Id),
                
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return authClaims;
        }
    }
}