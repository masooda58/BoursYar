using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jwt.Identity.Domain.Clients.Data;
using Jwt.Identity.Domain.Clients.Entity;
using Jwt.Identity.Framework.Response;

namespace Jwt.Identity.Data.Repositories.ClientRepository
{
    public class ClientRepositoryService : IClientRepository
    {
        public Task<IEnumerable<Client>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResultResponse> Add(Client entity)
        {
            throw new NotImplementedException();
        }

        public Task<ResultResponse> Update(Client entity)
        {
            throw new NotImplementedException();
        }

        public Task<ResultResponse> Delete(Client entity)
        {
            throw new NotImplementedException();
        }

        public Task<Client> FindByConditionAsync(Expression<Func<Client, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}