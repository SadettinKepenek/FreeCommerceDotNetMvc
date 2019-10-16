using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.Models
{
    public class RegisterModel
    {
        public Customer Customer { get; set; }
        public User User { get; set; }
    }
}