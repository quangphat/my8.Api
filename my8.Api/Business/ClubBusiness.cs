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
        MongoI.IClubRepository clubRepositoryM;
        NeoI.IClubRepository clubRepositoryN;
        public ClubBusiness(MongoI.IClubRepository clubRepoM, NeoI.IClubRepository clubRepoN)
        {
            clubRepositoryM = clubRepoM;
            clubRepositoryN = clubRepoN;
        }
        public async Task<bool> Create()
        {
            throw new NotImplementedException();
        }
    }
}
