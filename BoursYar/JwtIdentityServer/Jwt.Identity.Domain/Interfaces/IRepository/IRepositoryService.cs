using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jwt.Identity.Domain.Models.Client;
using Jwt.Identity.Domain.Models.Response;

namespace Jwt.Identity.Domain.Interfaces.IRepository
{
    public interface IRepositoryService<TEntity> where TEntity : class
    {
        public Task<IEnumerable<TEntity>> GetAllAsync();
     
        public Task<ResultResponse> Add(TEntity entity);
        public Task<ResultResponse> Update(TEntity entity);
        public Task<ResultResponse> Delete(TEntity entity);
        public Task<TEntity> FindByConditionAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
