using Jwt.Identity.Data.Context;
using Jwt.Identity.Domain.Interfaces.IUserRepositories;
using Jwt.Identity.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Jwt.Identity.Data.Repositories.UserRepositories
{
    public class UserManagementService : IUserManagementService

    {
        private readonly IdentityContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagementService(IdentityContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #region User-Role
        // GetUserRoleAsync باهم تست می شوند
        public async Task<List<string>> GetUserRoleAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return (List<string>)roles;
        }
        public async Task<List<string>> GetUserRoleAsync(string userIdOrEmail)
        {
            ApplicationUser user = await _context.Users.AsNoTracking().Where(u => u.Id == userIdOrEmail).FirstOrDefaultAsync() ??
                                   await _context.Users.AsNoTracking().Where(u => u.Email == userIdOrEmail).FirstOrDefaultAsync();
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return roles.ToList();
            }

            return null;

        }


        #endregion

        #region Get-USERS

        public async Task<int> GetAllUsersCountAsync(string searchString)
        {
            var users = _userManager.Users.AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
                users = users.Where(user => (user.LastName.Contains(searchString)
                                             || user.FirstName.Contains(searchString)
                                             || user.Email.Contains(searchString)));

            return await users.CountAsync();
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync(string searchString = null)
        {
            var users = _userManager.Users.AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
                users = users.Where(user => (user.LastName.Contains(searchString)
                                             || user.FirstName.Contains(searchString)
                                             || user.Email.Contains(searchString)));

            return await users.ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetUsersAsync(int offset, int limit, string sortOrder, string searchString)
        {
            offset = offset < 0 ? 0 : offset;
            limit = limit < 0 ? 0 : limit;


            var pageUsers = _userManager.Users.AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
                pageUsers = pageUsers.Where(user => (user.LastName.Contains(searchString)
                                                     || user.FirstName.Contains(searchString)
                                                     || user.Email.Contains(searchString)));

            switch (sortOrder)
            {
                case "Lname":
                    pageUsers = pageUsers.OrderBy(u => u.LastName);
                    break;
                case "Lname_desc":
                    pageUsers = pageUsers.OrderByDescending(u => u.LastName);
                    break;
                case "Fname":
                    pageUsers = pageUsers.OrderBy(u => u.FirstName);
                    break;
                case "Fname_desc":
                    pageUsers = pageUsers.OrderByDescending(u => u.FirstName);
                    break;
                case "Email":
                    pageUsers = pageUsers.OrderBy(u => u.Email);
                    break;
                case "Email_desc":
                    pageUsers = pageUsers.OrderByDescending(u => u.Email);
                    break;
                case "Approved":
                    pageUsers = pageUsers.OrderBy(u => u.Approved);
                    break;
                case "Approved_desc":
                    pageUsers = pageUsers.OrderByDescending(u => u.Approved);
                    break;
                default:
                    pageUsers = pageUsers.OrderBy(u => u.LastName);
                    break;
            }

            pageUsers = pageUsers.Skip(offset).Take(limit);

            return await pageUsers.ToListAsync();
        }
        public async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            return await _userManager.GetUserAsync(claimsPrincipal);
        }

        #endregion

        #region CRUD-USER

        public async Task<ApplicationUser> FindUserAsync(string userIdOrName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userIdOrName)
                       ?? await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userIdOrName);
            return user;
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> AddUserAsync(ApplicationUser user, string password, string role)
        {
            if (await _userManager.FindByEmailAsync(user.Email) != null)
                return IdentityResult.Failed(new IdentityError() { Description = "Email already in use!" });

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded && !string.IsNullOrEmpty(role))
                await _userManager.AddToRoleAsync(user, role);

            return result;
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user, string newUserRole)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser != null && existingUser.Id != user.Id)
                return IdentityResult.Failed(new IdentityError() { Description = "Email already in use!" });

            // _context.Entry(departmentToUpdate).Property("RowVersion").OriginalValue = rowVersion;
            _context.Entry(user).State = EntityState.Modified;


            // var result = await _userManager.UpdateAsync(user);
            // var result = _context.Users.Update(user);

            await _context.SaveChangesAsync();

            string[] existingRoles = (await _userManager.GetRolesAsync(user)).ToArray();
            var result = await _userManager.RemoveFromRolesAsync(user, existingRoles);

            if (result.Succeeded)
                result = await _userManager.AddToRoleAsync(user, newUserRole);

            return result;
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            return await _userManager.DeleteAsync(user);
        }
        public async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {

            return await _userManager.DeleteAsync(user);
        }

        #endregion


        #region Extra

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string newPassword)
        {
            var result = await _userManager.RemovePasswordAsync(user);
            if (result.Succeeded)
                result = await _userManager.AddPasswordAsync(user, newPassword);

            return result;
        }

        public async Task<bool> IsEmailInUseAsync(string email, string excludeUserId)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);

            return user != null && user.Id != excludeUserId;
        }

        public async Task<bool> IsEmailInUseAsync(string email)
        {
            return await IsEmailInUseAsync(email, null);
        }

        #endregion


    }
}
