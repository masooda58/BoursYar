using Jwt.Identity.Domain.Clients.Entity;
using Jwt.Identity.Domain.IServices;

namespace Jwt.Identity.Domain.Clients.Data
{
    public interface IClientRepository : IBaseRepositoryService<Client>
    {
    }
}