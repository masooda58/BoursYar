﻿using Jwt.Identity.Data.Context;
using Jwt.Identity.Domain.Clients.Data;
using Jwt.Identity.Domain.Clients.Entity;
using Jwt.Identity.Framework.Response;
using Jwt.Identity.Framework.Tools.PersianErrorHandelSqlException;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Jwt.Identity.Data.Repositories.ClientRepository
{
    public class ClientRepositoryService : IClientRepository
    {
        private readonly IdentityContext _context;

        public ClientRepositoryService(IdentityContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            try
            {
                return await _context.Clients.ToListAsync();
            }
            catch (Exception e)
            {
                throw ExceptionMessage.GetPerisanSqlExceptionMessage(e);
            }


        }

        public async Task<ResultResponse> Add(Client client)
        {
            try
            {
                var result = await _context.Clients.AddAsync(client);
                await _context.SaveChangesAsync();
                return new ResultResponse(true, "کلاینت با موفقیت اضافه گردید", client);
            }
            catch (Exception e)
            {
                throw ExceptionMessage.GetPerisanSqlExceptionMessage(e);

            }

        }

        public async Task<ResultResponse> Update(Client client)
        {
            try
            {
                var clientExist = await _context.Clients.FirstOrDefaultAsync(x => x.ClientName == client.ClientName.ToUpper());
                if (clientExist != null)
                {
                    clientExist = client;
                    await _context.SaveChangesAsync();
                    return new ResultResponse(true, "کلاینت آپدیت گردید", client);
                }
                return new ResultResponse(false, "کلاینت وجود ندارد", client);
            }
            catch (Exception e)
            {
                throw ExceptionMessage.GetPerisanSqlExceptionMessage(e);
            }
        }


        public async Task<ResultResponse> Delete(Client client)
        {
            try
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
                return new ResultResponse(true, "کلاینت با موفقیت حذف گردید", client);
            }
            catch (Exception e)
            {
                throw ExceptionMessage.GetPerisanSqlExceptionMessage(e);
            }
        }

        public Task<Client> FindByConditionAsync(Expression<Func<Client, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultResponse> GetByClientNameAsync(string clientName)
        {
            try
            {
                var client = await _context.Clients.FirstOrDefaultAsync(x => x.ClientName == clientName.ToUpper());
                return new ResultResponse(client != null, "Ok", client);
            }
            catch (Exception e)
            {
          

                throw ExceptionMessage.GetPerisanSqlExceptionMessage(e);
                //Exception ex = new Exception(result.Message.ToString());
                //ex.Data.Add("cus",JsonConvert.SerializeObject(ee));
                //throw result;
            }



        }

        public async Task<ResultResponse> DeleteClientNameAsync(string clientName)
        {
            try
            {
                var client = await _context.Clients.FirstOrDefaultAsync(x => x.ClientName == clientName.ToUpper());
                if (client != null)
                {
                    return await Delete(client);
                }

                throw new Exception("کلاینت وجود ندارد");
            }
            catch (Exception e)
            {
          

                throw ExceptionMessage.GetPerisanSqlExceptionMessage(e);
                //Exception ex = new Exception(result.Message.ToString());
                //ex.Data.Add("cus",JsonConvert.SerializeObject(ee));
                //throw result;
            }
        }
    }
}