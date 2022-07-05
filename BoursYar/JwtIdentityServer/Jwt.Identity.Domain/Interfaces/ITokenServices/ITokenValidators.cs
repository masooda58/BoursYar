namespace Jwt.Identity.Domain.Interfaces.ITokenServices
{
    public interface ITokenValidators
    {
        public bool Validate(string refreshToken);
    }
}
