using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Jwt.Identity.Domain.IServices.IUserServices;
using Jwt.Identity.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Jwt.Identity.Data.Repositories
{
    public class UserManagementService:IUserManagementService

    {
        public Task<List<string>> GetUserRoleAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetAllUsersCountAsync(string searchString)
        {
            throw new NotImplementedException();
        }

        public Task<List<ApplicationUser>> GetAllUsersAsync(string searchString)
        {
            throw new NotImplementedException();
        }

        public Task<List<ApplicationUser>> GetUsersAsync(int offset, int limit, string sortOrder, string searchString)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserRoleAsync(string userId, bool returnName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserRoleAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> AddUserAsync(ApplicationUser user, string password, string role)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateUserAsync(ApplicationUser user, string newUserRole, byte[] rowVersion)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEmailInUseAsync(string email, string excludeUserID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEmailInUseAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            throw new NotImplementedException();
        }
    }
}
