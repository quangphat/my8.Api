using my8.Api.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoI = my8.Api.Interfaces.Mongo;
using my8.Api.Models;
using AutoMapper;
namespace my8.Api.Business
{
    public class IndustryBusiness : IIndustryBusiness
    {
        MongoI.IIndustryRepository m_IndustryRepositoryM;
        public IndustryBusiness(MongoI.IIndustryRepository industryRepoM)
        {
            m_IndustryRepositoryM = industryRepoM;
        }
        public async Task<Industry> Create(Industry industry)
        {
            industry.Code = industry.Code.ToLower().Trim();
            string id = await m_IndustryRepositoryM.Create(industry);
            industry.Id = id;
            return industry;
        }

        public async Task<Industry> Get(string industryId)
        {
            return await m_IndustryRepositoryM.Get(industryId);
        }

        public async Task<Industry> GetByCode(string code)
        {
            return await m_IndustryRepositoryM.GetbyCode(code);
        }

        public async Task<List<Industry>> Gets()
        {
            return await m_IndustryRepositoryM.Gets();
        }

        public async Task<bool> Update(Industry industry)
        {
            return await m_IndustryRepositoryM.Update(industry);
        }
        public async Task<bool> Delete(string id)
        {
            return await m_IndustryRepositoryM.Delete(id);
        }

        public async Task<List<Industry>> Search(string searchStr)
        {
            searchStr = searchStr.ToLower();
            return await m_IndustryRepositoryM.Search(searchStr);
        }
    }
}
