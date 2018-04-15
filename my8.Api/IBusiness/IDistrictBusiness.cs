using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface IDistrictBusiness
    {
        Task<District> Create(District district);
        Task<District> Get(string districtId);
        Task<bool> Update(District district);
        Task<bool> Delete(string id);
        Task<List<District>> Search(string searchStr);
    }
}
