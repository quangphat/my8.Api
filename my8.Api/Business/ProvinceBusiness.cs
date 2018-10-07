using my8.Api.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoI = my8.Api.Interfaces.Mongo;
using NeoI = my8.Api.Interfaces.Neo4j;
using my8.Api.Models;
using AutoMapper;
using my8.Api.Infrastructures;

namespace my8.Api.Business
{
    public class ProvinceBusiness :BaseBusiness, IProvinceBusiness
    {
        MongoI.IProvinceRepository m_ProvinceRepositoryM;
        public ProvinceBusiness(MongoI.IProvinceRepository provinceRepoM,CurrentProcess process):base(process)
        {
            m_ProvinceRepositoryM = provinceRepoM;
        }
        public async Task<Province> Create(Province province)
        {
            string id = await m_ProvinceRepositoryM.Create(province);
            province.Id = id;
            return province;
        }

        public async Task<Province> Get(string provinceId)
        {
            return await m_ProvinceRepositoryM.Get(provinceId);
        }
        public async Task<bool> Update(Province province)
        {
            return await m_ProvinceRepositoryM.Update(province);
        }
        public async Task<bool> Delete(string id)
        {
            return await m_ProvinceRepositoryM.Delete(id);
        }
        public async Task<List<Province>> Search(string searchStr)
        {
            searchStr = searchStr.ToLower();
            return await m_ProvinceRepositoryM.Search(searchStr);
        }
    }
}
