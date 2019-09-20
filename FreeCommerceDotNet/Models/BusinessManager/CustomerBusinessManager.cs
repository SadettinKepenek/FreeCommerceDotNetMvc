using System;
using System.Collections.Generic;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class CustomerBusinessManager : IBusinessOperations<CustomerBM>
    {
        public int Add(CustomerBM entry)
        {
            using (CustomerManager manager = new CustomerManager())
            {
                return manager.Add(entry.Customer);
            }
        }

        public bool Delete(CustomerBM entry)
        {
            using (CustomerManager manager = new CustomerManager())
            {
                return manager.Delete(entry.Customer.CustomerId);
            }
        }

        public void Dispose()
        {
            
        }

        public List<CustomerBM> Get()
        {
            using (CustomerManager manager = new CustomerManager())
            {
                List<Customer> dbCustomers = manager.GetAll();
                List<CustomerBM> businessModels = new List<CustomerBM>();

                foreach (var customers in dbCustomers)
                {
                    businessModels.Add(new CustomerBM(customers.CustomerId));
                }

                return businessModels;
            }
        }

        public CustomerBM GetById(int id)
        {
            return new CustomerBM(id);
        }

        public bool Update(CustomerBM entry)
        {
            try
            {
                using (CustomerManager manager = new CustomerManager())
                {
                    manager.Update(entry.Customer);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}