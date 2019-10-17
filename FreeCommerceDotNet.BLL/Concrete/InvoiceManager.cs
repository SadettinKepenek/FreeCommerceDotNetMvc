using FreeCommerceDotNet.BLL.Abstract;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCommerceDotNet.BLL.Concrete
{
    public class InvoiceManager : IInvoiceService
    {
        private readonly IInvoiceDal _invoiceDal;

        public InvoiceManager()
        {

        }

        public InvoiceManager(IInvoiceDal invoiceDal)
        {
            _invoiceDal = invoiceDal;
        }
        public ServiceResult Insert(Invoice entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_invoiceDal.Insert(entity));
        }
        public ServiceResult Update(Invoice entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_invoiceDal.Update(entity));

        }
        public ServiceResult Delete(int id)
        {
            return ServiceHelper.FromDbResultToServiceResult(_invoiceDal.Delete(id));

        }

        public void Dispose()
        {
           
        }


        public List<Invoice> SelectAll()
        {
            return _invoiceDal.SelectAll();

        }

        public List<Invoice> SelectByFilter(List<DBFilter> filters)
        {
            return _invoiceDal.SelectByFilter(filters);

        }

        public Invoice SelectById(int id)
        {
            return _invoiceDal.SelectById(id);

        }


    }
}
