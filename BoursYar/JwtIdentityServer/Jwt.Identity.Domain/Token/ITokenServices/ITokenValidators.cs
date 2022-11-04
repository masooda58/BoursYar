namespace Jwt.Identity.Domain.Token.ITokenServices
{
    public interface ITokenValidators
    {
        public bool Validate(string refreshToken);
    }
}