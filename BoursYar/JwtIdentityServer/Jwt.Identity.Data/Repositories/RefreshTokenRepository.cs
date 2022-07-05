using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jwt.Identity.Domain.IServices.IUserServices;

namespace Jwt.Identity.Data.Repositories
{
   public  class RefreshTokenRepository:IRefreshTokenRepository
    {
        public Task<bool> WritRefreshTokenAsync(string userId, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdByRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRefreshTokenByuserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
