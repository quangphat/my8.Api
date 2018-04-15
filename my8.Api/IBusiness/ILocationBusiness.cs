using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface ILocationBusiness
    {
        Task<Location> Create(Location location);
        Task<Location> Get(string locationId);
        Task<bool> Update(Location location);
        Task<bool> Delete(string id);
        Task<List<Location>> Search(string searchStr);
    }
}
