using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Jwt.Identity.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Jwt.Identity.Domain.Interfaces.IUserRepositories
{
    public interface IRoleManagementService
    {

        Task<IdentityResult> CreateRoleAsync(string roleName);
        Task<IdentityResult> DeleteRoleAsync(IdentityRole role);
        void DeleteRolesByNameAsync(List<string> rolesName);
        Task<List<IdentityRole>> GetAllRolesAsync(string searchRoleName);
        Task<IdentityRole> FindRoleByNameAsync(string name);
        Task<IdentityRole> FindRoleByIdAsync(string roleId);
        Task<List<Claim>> GetClaimsByRoleNameAsync(string roleName);
        Task<List<Claim>> GetClaimsByRoleAsync(IdentityRole role);
        Task<IdentityResult> AddClaimToRoleAsync(IdentityRole role, Claim claim);
        Task<IdentityResult> AddClaimsToRoleAsync(IdentityRole role, List<Claim> claims);
        Task<IdentityResult> RemoveClaimsToRoleAsync(IdentityRole role, List<Claim>claims);

        Task<IdentityResult> UpdateAsync(IdentityRole role);
        Task<IdentityResult> SetRoleNameAsync(IdentityRole role, string newRoleName);

        Task<IdentityResult> CreateRoleAsync(IdentityRole role);
    }
}
