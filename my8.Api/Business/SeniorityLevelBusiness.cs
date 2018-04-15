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
    public class SeniorityLevelBusiness : ISeniorityLevelBusiness
    {
        MongoI.ISeniorityLevelRepository m_SeniorityLevelRepositoryM;
        public SeniorityLevelBusiness(MongoI.ISeniorityLevelRepository senioritylevelRepoM)
        {
            m_SeniorityLevelRepositoryM = senioritylevelRepoM;
        }
        public async Task<SeniorityLevel> Create(SeniorityLevel senioritylevel)
        {
            string id = await m_SeniorityLevelRepositoryM.Create(senioritylevel);
            senioritylevel.Id = id;
            return senioritylevel;
        }

        public async Task<SeniorityLevel> Get(string senioritylevelId)
        {
            return await m_SeniorityLevelRepositoryM.Get(senioritylevelId);
        }
        public async Task<bool> Update(SeniorityLevel senioritylevel)
        {
            return await m_SeniorityLevelRepositoryM.Update(senioritylevel);
        }
        public async Task<bool> Delete(string id)
        {
            return await m_SeniorityLevelRepositoryM.Delete(id);
        }
        public async Task<List<SeniorityLevel>> Search(string searchStr)
        {
            searchStr = searchStr.ToLower();
            return await m_SeniorityLevelRepositoryM.Search(searchStr);
        }

        public async Task<List<SeniorityLevel>> Gets()
        {
            return await m_SeniorityLevelRepositoryM.Gets();
        }
    }
}
