using System;
using System.Threading.Tasks;
using Jwt.Identity.Domain.Interfaces.IUserRepositories;
using Jwt.Identity.Domain.Models;

namespace Jwt.Identity.Data.Repositories.UserRepositories
{
    public class RoleManagementService:IRoleManagementService
    {
        public Task<ApplicationRole> GetRoleByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
