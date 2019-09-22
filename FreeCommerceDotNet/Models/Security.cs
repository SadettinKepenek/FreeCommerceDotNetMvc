using System.ComponentModel.DataAnnotations;

namespace FreeCommerceDotNet.Models
{
    public class Security
    {
        
    }

    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public bool rememberMe { get; set; }
        public string EMail { get; set; }
        public string Roles { get; set; }

    }
}