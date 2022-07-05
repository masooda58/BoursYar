using System.Threading.Tasks;

namespace Jwt.Identity.Domain.IServices.IUserServices
{
    public interface IRefreshTokenRepository
    {
        Task<bool> WritRefreshTokenAsync(string userId,string refreshToken);
        Task<string> GetUserIdByRefreshToken(string refreshToken);
        Task<bool> DeleteRefreshTokenByuserId(string userId);
    }
}
