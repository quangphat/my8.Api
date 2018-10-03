using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Models
{
    public class Pagination<T> where T : class
    {
        public List<T> Datas { get; set; }
        public int TotalRecord { get; set; }
    }
}
