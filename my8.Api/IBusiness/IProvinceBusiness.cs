using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface IProvinceBusiness
    {
        Task<Province> Create(Province province);
        Task<Province> Get(string provinceId);
        Task<bool> Update(Province province);
        Task<bool> Delete(string id);
        Task<List<Province>> Search(string searchStr);
    }
}
