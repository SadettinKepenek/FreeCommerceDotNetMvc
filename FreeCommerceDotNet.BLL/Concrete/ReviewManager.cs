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
    public class ReviewManager : IReviewService
    {
        private IReviewDal _reviewRepository;
        public ReviewManager() { }

        public ReviewManager(IReviewDal reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public ServiceResult Insert(Reviews entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_reviewRepository.Insert(entity));
        }
        public ServiceResult Update(Reviews entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_reviewRepository.Update(entity));

        }
        public ServiceResult Delete(int id)
        {
            return ServiceHelper.FromDbResultToServiceResult(_reviewRepository.Delete(id));

        }


        public List<Reviews> SelectAll()
        {
            return _reviewRepository.SelectAll();

        }

        public List<Reviews> SelectByFilter(List<DBFilter> filters)
        {
            return _reviewRepository.SelectByFilter(filters);
        }

        public Reviews SelectById(int id)
        {
            return _reviewRepository.SelectById(id);
        }


        public void Dispose()
        {
        }
    }
}
