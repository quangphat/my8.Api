using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.IBusiness
{
    interface IPersonBusiness
    {
        Task<bool> Create();
    }
}
