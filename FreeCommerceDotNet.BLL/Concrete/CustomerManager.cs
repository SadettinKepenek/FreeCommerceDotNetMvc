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
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _customerRepository;
        public CustomerManager() { }

        public CustomerManager(ICustomerDal customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public ServiceResult Insert(Customer entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_customerRepository.Insert(entity));
        }
        public ServiceResult Update(Customer entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_customerRepository.Update(entity));

        }
        public ServiceResult Delete(int id)
        {
            return ServiceHelper.FromDbResultToServiceResult(_customerRepository.Delete(id));

        }


        public List<Customer> SelectAll()
        {
            return _customerRepository.SelectAll();

        }

        public List<Customer> SelectByFilter(List<DBFilter> filters)
        {
            return _customerRepository.SelectByFilter(filters);
        }

        public Customer SelectById(int id)
        {
            return _customerRepository.SelectById(id);
        }
    }
}
