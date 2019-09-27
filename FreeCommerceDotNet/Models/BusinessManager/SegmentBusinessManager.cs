using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class SegmentBusinessManager : IBusinessOperations<SegmentBM>
    {
        public int Add(SegmentBM entry)
        {
            using (SegmentManager manager = new SegmentManager())
            {
                return manager.Add(entry.Segment);
            }
        }

        public bool Delete(SegmentBM entry)
        {
            using (SegmentManager manager = new SegmentManager())
            {
                return manager.Delete(entry.Segment.SegmentId);
            }
        }

        public void Dispose()
        {
           
        }

        public List<SegmentBM> Get()
        {
            using (SegmentManager manager = new SegmentManager())
            {
                List<Segment> dbSegments = manager.GetAll();
                List<SegmentBM> businessModels = new List<SegmentBM>();

                foreach (var segments in dbSegments)
                {
                    businessModels.Add(new SegmentBM(segments.SegmentId));
                }

                return businessModels;
            }
        }

        public SegmentBM GetById(int id)
        {
            return new SegmentBM(id);
        }

        public bool Update(SegmentBM entry)
        {
            try
            {
                using (SegmentManager manager = new SegmentManager())
                {
                    manager.Update(entry.Segment);
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