using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Models.Mongo
{
    public class CommercialAdsPost:StatusPost
    {
        public int MaxTargetPerson { get; set; }
        public decimal Expense { get; set; }
    }
}
