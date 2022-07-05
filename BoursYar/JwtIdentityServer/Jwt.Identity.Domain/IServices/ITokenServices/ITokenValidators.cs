namespace Jwt.Identity.Domain.IServices.ITokenServices
{
    public interface ITokenValidators
    {
        public bool Validate(string refreshToken);
    }
}
