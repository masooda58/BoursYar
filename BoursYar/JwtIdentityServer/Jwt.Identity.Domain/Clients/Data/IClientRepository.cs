using System.Threading.Tasks;
using Jwt.Identity.Domain.Clients.Entity;
using Jwt.Identity.Domain.IServices;
using Jwt.Identity.Framework.Response;

namespace Jwt.Identity.Domain.Clients.Data
{
    public interface IClientRepository : IBaseRepositoryService<Client>
    {
        public Task<ResultResponse> GetByClientNameAsync(string clientName);
        public Task<ResultResponse> DeleteClientNameAsync(string clientName);
    }
}