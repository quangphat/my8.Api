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
    public class CommunityBusiness :BaseBusiness, ICommunityBusiness
    {
        MongoI.ICommunityRepository m_CommunityRepositoryM;
        NeoI.ICommunityRepository m_CommunityRepositoryN;
        public CommunityBusiness(MongoI.ICommunityRepository CommunityRepoM, NeoI.ICommunityRepository CommunityRepoN,CurrentProcess process):base(process)
        {
            m_CommunityRepositoryM = CommunityRepoM;
            m_CommunityRepositoryN = CommunityRepoN;
        }
        public async Task<Community> Create(Community Community)
        {
            string id = await m_CommunityRepositoryM.Create(Community);
            if (string.IsNullOrWhiteSpace(id)) return null;
            Community.CommunityId = id;
            bool t = await m_CommunityRepositoryN.Create(Community);
            if(t)
            {
                return Community;
            }
            return null;
        }
        public async Task<bool> Update(Community Community)
        {
            bool result = await m_CommunityRepositoryN.Update(Community);
            return result;
        }
        public async Task<CommunityAllin> Get(string id)
        {
            CommunityAllin Community = await m_CommunityRepositoryN.Get(id);
            return Community;
        }
        public async Task<List<CommunityAllin>> Search(string searchStr, int skip, int limit)
        {
            IEnumerable<CommunityAllin> lstCommunity = await m_CommunityRepositoryN.Search(searchStr, skip, limit);
            return lstCommunity.ToList();
        }
        public async Task<bool> KickOutMember(string CommunityId,string personId)
        {
            bool result = await m_CommunityRepositoryN.KickOutMember(CommunityId, personId);
            return result;
        }
        public async Task<bool> AddMember(string CommunityId, string personId)
        {
            bool result = await m_CommunityRepositoryN.AddMember(CommunityId, personId);
            return result;
        }
       public async Task<List<PersonAllin>> GetMembers(string CommunityId)
        {
            IEnumerable<PersonAllin> lstPerson = await m_CommunityRepositoryN.GetMembers(CommunityId);
            return lstPerson.ToList();
        }
    }
}
