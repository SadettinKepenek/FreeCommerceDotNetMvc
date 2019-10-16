using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCommerceDotNet.DAL.Abstract
{
    public interface IUserDal : IRepository<User>
    {
        ResetTicket SelectResetTicket(int ticketid);
        DBResult InsertResetTicket(int userid);
        DBResult UpdateResetTicket(int ticketid);


    }
}
