using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.IBusiness
{
    public interface IPageBusiness
    {
        Task<bool> Create();
    }
}
