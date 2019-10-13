using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class Wish:IEntity
    {
        public int WishId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string WishDate { get; set; }
        public Customer customer { get; set; }
        public Product product { get; set; }

    }
}