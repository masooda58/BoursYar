namespace Jwt.Identity.Domain.Interfaces.IPhoneTotpProvider
{
    public interface IPhoneTotpProvider
    {
        public string GenerateTotp(string secretKey);
        public bool VerifyTotp(string secretKey, string code);
    }
}
