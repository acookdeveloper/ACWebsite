﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACWebsite.Models
{
    public class returnStats
    {
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double last { get; set; }
        public double growthtoday { get; set; }
        public double growthtodaypct { get; set; }
        public double volume { get; set; }
        public double volume_30day { get; set; }
    }
}