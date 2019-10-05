﻿using System.Collections.Generic;
using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class Segment:IEntity
    {
        public int SegmentId { get; set; }
        public string SegmentName { get; set; }
        public string Priorty { get; set; }
        public List<Customer> CustomersBms { get; set; }

    }
}