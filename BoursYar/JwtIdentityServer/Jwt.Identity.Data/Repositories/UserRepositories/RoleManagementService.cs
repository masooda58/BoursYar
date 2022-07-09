#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Jwt.Identity.Data.Context;
using Jwt.Identity.Domain.Interfaces.IUserRepositories;
using Jwt.Identity.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Jwt.Identity.Data.Repositories.UserRepositories
{
    public class RoleManagementService:IRoleManagementService
    {
        private readonly IdentityContext _context;
       
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleManagementService(IdentityContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            
            var existRole = await _roleManager.RoleExistsAsync(roleName);
            if (existRole)
            {
                return IdentityResult.Failed(new IdentityError() { Description = "Role name already in use!" });
            }

            var role = new IdentityRole()
            {
                Name = roleName,
            };
            return await _roleManager.CreateAsync(role);
        }
        public async Task<IdentityResult> CreateRoleAsync(IdentityRole role)
        {
            var roleExist = await _roleManager.RoleExistsAsync(role.Name);
            if(!roleExist)
            {
                return await _roleManager.CreateAsync(role);
            }
            return IdentityResult.Failed
                (new IdentityError(){ Description = "Role name already in use!" });
        }

        public async Task<IdentityResult> DeleteRoleAsync(IdentityRole role)
        {
            return await _roleManager.DeleteAsync(role);
        }
      
        public async Task<bool> DeleteRolesByNameAsync(List<string> rolesName)
        {
           // var results = new List<IdentityResult>();
         

          
               foreach (var name in rolesName)
               {
                   var role = await _roleManager.FindByNameAsync(name);
                   if (role!=null)
                   {
                       var result = await _roleManager.DeleteAsync(role);
                   }
                       
                

               }

               return true;

        }
         

        public async Task<List<IdentityRole>> GetAllRolesAsync(string? searchRoleName=null)
        {
            if (!string.IsNullOrEmpty(searchRoleName))
            {
                return await _context.Roles.AsNoTracking().Where(role => role.Name.Contains(searchRoleName))
                    .ToListAsync();
            }
            return await _context.Roles.AsNoTracking().ToListAsync();
        }

        public async Task<IdentityRole> FindRoleByNameAsync(string name)
        {
            return await _roleManager.FindByNameAsync(name);
        }

        public async Task<IdentityRole> FindRoleByIdAsync(string roleId)
        {
            return await _roleManager.FindByIdAsync(roleId);
        }

        
        public async Task<IdentityResult> AddClaimToRoleAsync(IdentityRole role, Claim claim)
        {
            return await _roleManager.AddClaimAsync(role, claim);
        }

        public async Task<List<IdentityResult>> AddClaimsToRoleAsync(IdentityRole role, List<Claim> claims)
        {
            var result = new List<IdentityResult>();
         
                foreach (var claim in claims)
                {
                    var resultOfClaim=await _roleManager.AddClaimAsync(role, claim);
                    result.Add(resultOfClaim);
                }

                return result;




        }
        //start heare
        public async Task<IdentityResult> RemoveClaimsToRoleAsync(IdentityRole role, List<Claim> claims)
        {
            try
            {
                foreach (var claim in claims)
                {
                    await _roleManager.RemoveClaimAsync(role,claim);
                }

                return IdentityResult.Success;
            }
            catch
            {
                return IdentityResult.Failed();
            }
        }

        public async Task<List<Claim>> GetClaimsByRoleNameAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return ( List<Claim>)await _roleManager.GetClaimsAsync(role);

        }

        public async Task<List<Claim>> GetClaimsByRoleAsync(IdentityRole role)
        {
            return ( List<Claim>)await _roleManager.GetClaimsAsync(role);
        }
        public async Task<IdentityResult> UpdateAsync(IdentityRole role)
        {
            role.Name = "test";
            return await _roleManager.UpdateAsync(role);
        }

        public async Task<IdentityResult> SetRoleNameAsync(IdentityRole role, string newRoleName)
        {
            return await _roleManager.SetRoleNameAsync(role,newRoleName);
        }
    }
}
