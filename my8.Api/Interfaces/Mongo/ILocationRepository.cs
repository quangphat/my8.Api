using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface ILocationRepository
    {
        Task<string> Create(Location location);
        Task<Location> Get(string id);
        Task<bool> Update(Location location);
        Task<bool> Delete(string id);
        Task<List<Location>> Search(string searchStr);
    }
}

