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
    public class SegmentManager : ISegmentService
    {
        private ISegmentDal _segmentRepository;
        public SegmentManager() { }

        public SegmentManager(ISegmentDal segmentRepository)
        {
            _segmentRepository = segmentRepository;
        }

        public ServiceResult Insert(Segment entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_segmentRepository.Insert(entity));
        }
        public ServiceResult Update(Segment entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_segmentRepository.Update(entity));

        }
        public ServiceResult Delete(int id)
        {
            return ServiceHelper.FromDbResultToServiceResult(_segmentRepository.Delete(id));

        }


        public List<Segment> SelectAll()
        {
            return _segmentRepository.SelectAll();

        }

        public List<Segment> SelectByFilter(List<DBFilter> filters)
        {
            return _segmentRepository.SelectByFilter(filters);
        }

        public Segment SelectById(int id)
        {
            return _segmentRepository.SelectById(id);
        }

        public void Dispose()
        {
        }
    }
}
