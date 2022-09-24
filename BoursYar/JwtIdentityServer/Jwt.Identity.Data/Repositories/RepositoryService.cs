using Jwt.Identity.Domain.Interfaces.IRepository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jwt.Identity.Data.Context;
using Jwt.Identity.Domain.Models.Response;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace Jwt.Identity.Data.Repositories
{
    public class RepositoryService<TEntity> : IRepositoryService<TEntity> where TEntity : class
    {
        private readonly IdentityContext _context;

        public RepositoryService(IdentityContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }



        public async Task<ResultResponse> Add(TEntity entity)
        {

            try
            {
                var x= await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return new ResultResponse(true, "");
            }
            catch (Exception e)
            {
                if(e.InnerException.ToString().Contains("insert duplicate"))
                    return new ResultResponse(false,"insert duplicate");
                return new ResultResponse(false, e.InnerException.ToString());
            }
               
            
       
        }

        public  async Task<ResultResponse> Update(TEntity entity)
        {
            try
            {
                
                _context.Set<TEntity>();
                _context.Entry(entity).State = EntityState.Modified;
              var result=  await _context.SaveChangesAsync();
                return result !=0?new ResultResponse(true, ""):new ResultResponse(false,"");
            }
            catch (Exception e)
            {
                return e.InnerException.ToString().Contains("insert duplicate") ?
                    new ResultResponse(false,"insert duplicate") :
                    new ResultResponse(false, e.InnerException.ToString());
            }
        }

        public async Task<ResultResponse> Delete(TEntity entity)
        {

            try
            {
                var deleteResult = _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
                return deleteResult !=null?new ResultResponse(true, ""):new ResultResponse(false,"");;
            }
            catch (Exception e)
            {
                return new ResultResponse(false, e.InnerException.ToString());
            }
        }

        public async Task<TEntity> FindByConditionAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }
    }
}
