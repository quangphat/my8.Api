using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IDistrictRepository
    {
        Task<string> Create(District district);
        Task<District> Get(string id);
        Task<bool> Update(District district);
        Task<bool> Delete(string id);
        Task<List<District>> Search(string searchStr);
    }
}

