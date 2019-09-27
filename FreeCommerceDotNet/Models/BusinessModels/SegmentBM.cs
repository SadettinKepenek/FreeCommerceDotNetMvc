using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class SegmentBM
    {
        public Segment Segment { get; set; }

        public List<Customer> CustomersBms { get; set; }

        public SegmentBM()
        {
            Segment = new Segment();

        }
        public SegmentBM(int? id)
        {
            if (id == null)
            {
                Segment = new Segment();
            }
            else
            {
                using (var m = new SegmentManager())
                {
                    int key = (int)id;
                    Segment = m.Get(key);
                }

                using(var m = new CustomerManager())
                {
                    CustomersBms =
                        m.GetByIntegerKey((int)id, "Customer", "SegmentId");
                }
            }
        }

    }
}