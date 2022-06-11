#nullable enable
using System.ComponentModel.DataAnnotations;

namespace IdentityApi.Models.Requests
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "نام کاربری را وراد کنید")]
        public string? Username { get; set; }

        [EmailAddress(ErrorMessage = "ایمیل معتبر نیست")]
        [Required(ErrorMessage = "ایمیل را وارد کنید")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
