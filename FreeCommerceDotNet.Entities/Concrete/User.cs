using System.ComponentModel.DataAnnotations;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username Zorunludur")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password Zorunludur")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email Zorunludur")]
        public string EMail { get; set; }
        [Required(ErrorMessage = "Role Zorunludur")]
        public string Role { get; set; }
    }
}