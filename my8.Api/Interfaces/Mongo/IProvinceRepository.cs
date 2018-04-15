using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IProvinceRepository
    {
        Task<string> Create(Province province);
        Task<Province> Get(string id);
        Task<bool> Update(Province province);
        Task<bool> Delete(string id);
        Task<List<Province>> Search(string searchStr);
    }
}

