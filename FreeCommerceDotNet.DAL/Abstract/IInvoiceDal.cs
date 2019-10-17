using FreeCommerceDotNet.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCommerceDotNet.DAL.Abstract
{
    public interface IInvoiceDal : IRepository<Invoice>
    {
        int GetInvoiceCount();
    }
}
