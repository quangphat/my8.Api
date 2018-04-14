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
    public class ClubBusiness : IClubBusiness
    {
        MongoI.IClubRepository m_ClubRepositoryM;
        NeoI.IClubRepository m_ClubRepositoryN;
        public ClubBusiness(MongoI.IClubRepository clubRepoM, NeoI.IClubRepository clubRepoN)
        {
            m_ClubRepositoryM = clubRepoM;
            m_ClubRepositoryN = clubRepoN;
        }
        public async Task<Club> Create(Club club)
        {
            string id = await m_ClubRepositoryM.Create(club);
            if (string.IsNullOrWhiteSpace(id)) return null;
            club.ClubId = id;
            bool t = await m_ClubRepositoryN.Create(club);
            if(t)
            {
                return club;
            }
            return null;
        }
        public async Task<bool> Update(Club club)
        {
            bool result = await m_ClubRepositoryN.Update(club);
            return result;
        }
        public async Task<ClubAllin> Get(string id)
        {
            ClubAllin club = await m_ClubRepositoryN.Get(id);
            return club;
        }
        public async Task<List<ClubAllin>> Search(string searchStr, int skip, int limit)
        {
            IEnumerable<ClubAllin> lstClub = await m_ClubRepositoryN.Search(searchStr, skip, limit);
            return lstClub.ToList();
        }
        public async Task<bool> KickOutMember(string clubId,string personId)
        {
            bool result = await m_ClubRepositoryN.KickOutMember(clubId, personId);
            return result;
        }
        public async Task<bool> AddMember(string clubId, string personId)
        {
            bool result = await m_ClubRepositoryN.AddMember(clubId, personId);
            return result;
        }
       public async Task<List<PersonAllin>> GetMembers(string clubId)
        {
            IEnumerable<PersonAllin> lstPerson = await m_ClubRepositoryN.GetMembers(clubId);
            return lstPerson.ToList();
        }
    }
}
