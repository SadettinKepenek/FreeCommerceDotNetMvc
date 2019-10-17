using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCommerceDotNet.BLL.Abstract
{
    public interface IInvoiceService : IService<Invoice>
    {
        int GetInvoiceCount();
        string GetTranscationNumber();
    }
}
