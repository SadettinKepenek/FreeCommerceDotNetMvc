using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class ServiceResult:IEntity
    {
        public int Id { get; set; }
        public string Message { get; set; }

    }
}