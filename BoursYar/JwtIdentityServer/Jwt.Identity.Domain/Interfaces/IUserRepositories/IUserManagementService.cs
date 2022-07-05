﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Jwt.Identity.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Jwt.Identity.Domain.Interfaces.IUserRepositories
{
    public interface IUserManagementService
    {
        Task<List<string>> GetUserRoleAsync(ApplicationUser user);
        Task<int> GetAllUsersCountAsync(string searchString);
        Task<List<ApplicationUser>> GetAllUsersAsync(string searchString);
        // Task<PaginatedList<ApplicationUser>> GetAllUsersPaginatedAsync(int pageIndex, int pageSize, string searchString, string sortOrder);
        Task<List<ApplicationUser>> GetUsersAsync(int offset, int limit, string sortOrder, string searchString);
        Task<string> GetUserRoleAsync(string userId, bool returnName);
        Task<string> GetUserRoleAsync(string email);
        Task<ApplicationUser> FindUserAsync(string userId);
        Task<IdentityResult> AddUserAsync(ApplicationUser user, string password, string role);
        Task<IdentityResult> UpdateUserAsync(ApplicationUser user, string newUserRole, byte[] rowVersion);
        Task<IdentityResult> DeleteUserAsync(string userId);
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string password);
        Task<bool> IsEmailInUseAsync(string email, string excludeUserID);
        Task<bool> IsEmailInUseAsync(string email);
        Task<ApplicationUser> GetUserAsync(ClaimsPrincipal claimsPrincipal);

       

    }
}