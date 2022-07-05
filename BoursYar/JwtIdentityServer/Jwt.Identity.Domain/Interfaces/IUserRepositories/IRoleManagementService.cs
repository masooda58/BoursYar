using System.Threading.Tasks;
using Jwt.Identity.Domain.Models;

namespace Jwt.Identity.Domain.Interfaces.IUserRepositories
{
    public interface IRoleManagementService
    {
        Task<ApplicationRole> GetRoleByNameAsync(string name);
 
     
    }
}
