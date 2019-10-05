using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCommerceDotNet.DAL.Concrete
{
    public class OrderDetailRepository : IOrderDetailDal
    {
        public DBResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DBResult Insert(OrderDetail entity)
        {
            throw new NotImplementedException();
        }

        public List<OrderDetail> SelectAll()
        {
            throw new NotImplementedException();
        }

        public List<OrderDetail> SelectByFilter(List<DBFilter> filters)
        {
            throw new NotImplementedException();
        }

        public OrderDetail SelectById(int id)
        {
            throw new NotImplementedException();
        }

        public DBResult Update(OrderDetail entity)
        {
            throw new NotImplementedException();
        }
    }
}
