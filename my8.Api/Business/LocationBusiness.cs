using my8.Api.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoI = my8.Api.Interfaces.Mongo;
using NeoI = my8.Api.Interfaces.Neo4j;
using my8.Api.Models;
using AutoMapper;
namespace my8.Api.Business
{
    public class LocationBusiness : ILocationBusiness
    {
        MongoI.ILocationRepository m_LocationRepositoryM;
        public LocationBusiness(MongoI.ILocationRepository locationRepoM)
        {
            m_LocationRepositoryM = locationRepoM;
        }
        public async Task<Location> Create(Location location)
        {
            string id = await m_LocationRepositoryM.Create(location);
            location.Id = id;
            return location;
        }

        public async Task<Location> Get(string locationId)
        {
            return await m_LocationRepositoryM.Get(locationId);
        }
        public async Task<bool> Update(Location location)
        {
            return await m_LocationRepositoryM.Update(location);
        }
        public async Task<bool> Delete(string id)
        {
            return await m_LocationRepositoryM.Delete(id);
        }
        public async Task<List<Location>> Search(string searchStr)
        {
            searchStr = searchStr.ToLower();
            return await m_LocationRepositoryM.Search(searchStr);
        }
    }
}
