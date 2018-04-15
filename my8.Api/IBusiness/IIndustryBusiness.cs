using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface IIndustryBusiness
    {
        Task<Industry> Create(Industry industry);
        Task<Industry> Get(string industryId);
        Task<Industry> GetByCode(string code);
        Task<List<Industry>> Gets();
        Task<bool> Update(Industry industry);
        Task<bool> Delete(string id);
        Task<List<Industry>> Search(string searchStr);
    }
}
