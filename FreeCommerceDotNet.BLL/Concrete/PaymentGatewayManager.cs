using System.Collections.Generic;
using FreeCommerceDotNet.BLL.Abstract;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.BLL.Concrete
{
    public class PaymentGatewayManager:IPaymentGatewayService
    {
        private readonly IPaymentGatewayDal _paymentGatewayDal;

        public PaymentGatewayManager()
        {
            
        }

        public PaymentGatewayManager(IPaymentGatewayDal paymentGatewayDal)
        {
            _paymentGatewayDal = paymentGatewayDal;
        }
        public ServiceResult Insert(Payment entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_paymentGatewayDal.Insert(entity));
        }

        public ServiceResult Update(Payment entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_paymentGatewayDal.Update(entity));
        }

        public ServiceResult Delete(int id)
        {
            return ServiceHelper.FromDbResultToServiceResult(_paymentGatewayDal.Delete(id));
        }

        public Payment SelectById(int id)
        {
            return _paymentGatewayDal.SelectById(id);
        }

        public List<Payment> SelectByFilter(List<DBFilter> filters)
        {
            return _paymentGatewayDal.SelectByFilter(filters);
        }

        public List<Payment> SelectAll()
        {
            return _paymentGatewayDal.SelectAll();
        }

        public void Dispose()
        {
        }
    }
}