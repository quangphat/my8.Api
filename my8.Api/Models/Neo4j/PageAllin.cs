using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Models.Neo4j
{
    public class PageAllin
    {
        public Page Page { get; set; }
        public int Total { get; set; }//for paging
    }
}
