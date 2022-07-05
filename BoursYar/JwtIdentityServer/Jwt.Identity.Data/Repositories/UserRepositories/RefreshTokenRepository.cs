using System;
using System.Threading.Tasks;
using Jwt.Identity.Domain.Interfaces.IUserRepositories;

namespace Jwt.Identity.Data.Repositories.UserRepositories
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
