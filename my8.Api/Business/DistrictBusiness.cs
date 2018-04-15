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
    public class DistrictBusiness : IDistrictBusiness
    {
        MongoI.IDistrictRepository m_DistrictRepositoryM;
        public DistrictBusiness(MongoI.IDistrictRepository districtRepoM)
        {
            m_DistrictRepositoryM = districtRepoM;
        }
        public async Task<District> Create(District district)
        {
            string id = await m_DistrictRepositoryM.Create(district);
            district.Id = id;
            return district;
        }

        public async Task<District> Get(string districtId)
        {
            return await m_DistrictRepositoryM.Get(districtId);
        }
        public async Task<bool> Update(District district)
        {
            return await m_DistrictRepositoryM.Update(district);
        }
        public async Task<bool> Delete(string id)
        {
            return await m_DistrictRepositoryM.Delete(id);
        }
        public async Task<List<District>> Search(string searchStr)
        {
            searchStr = searchStr.ToLower();
            return await m_DistrictRepositoryM.Search(searchStr);
        }
    }
}
