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
        public async Task<Club> Get(string id)
        {
            return await m_ClubRepositoryN.Get(id);
        }
    }
}
