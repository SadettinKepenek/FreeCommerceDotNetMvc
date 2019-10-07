using FreeCommerceDotNet.Entities.Concrete;
using System;
using System.Reflection;
using System.Text;

namespace FreeCommerceDotNet.Common.Concrete
{
    public static class ServiceHelper
    {
        public static ServiceResult FromDbResultToServiceResult(DBResult dbResult)
        {
            return new ServiceResult()
            {
                Id = dbResult.Id,
                Message = dbResult.Message
            };
        }

        public static ServiceResult CreateServiceResultMessage(ServiceReturn ResultMessageType,int id=-1)
        {
            var serviceResultMessage = new ServiceResult()
            {
                Id = id,

            };
            switch (ResultMessageType)
            {
                case ServiceReturn.ParameterError:
                    serviceResultMessage.Message = "ServiceReturnParameterError";
                    break;
                case ServiceReturn.Success:
                    serviceResultMessage.Message = "ServiceOperatioSuccess";
                    break;
            }
            return serviceResultMessage;
        }
    
    }
}