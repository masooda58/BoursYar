using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jwt.Identity.Domain.IServices.IUserServices;
using Jwt.Identity.Domain.Models;

namespace Jwt.Identity.Data.Repositories
{
    public class RoleManagementService:IRoleManagementService
    {
        public Task<ApplicationRole> GetRoleByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
