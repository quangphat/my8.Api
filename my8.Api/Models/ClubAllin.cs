﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Models
{
    public class CommunityAllin
    {
        public Community Community {get;set;}
        public int Joins { get; set; }
        public int Total { get; set; }
    }
}
