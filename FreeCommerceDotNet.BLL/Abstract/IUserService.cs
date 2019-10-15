using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.BLL.Abstract
{
    public interface IUserService:IService<User>
    {
        ResetTicket SelectResetTicket(int userid, int ticketid);
        ServiceResult InsertResetTicket(int userid);
        ServiceResult UpdateResetTicket(int ticketid);
    }
}