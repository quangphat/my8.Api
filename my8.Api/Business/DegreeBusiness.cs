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
    public class DegreeBusiness : IDegreeBusiness
    {
        MongoI.IDegreeRepository m_DegreeRepositoryM;
        public DegreeBusiness(MongoI.IDegreeRepository degreeRepoM)
        {
            m_DegreeRepositoryM = degreeRepoM;
        }
        public async Task<Degree> Create(Degree degree)
        {
            string id = await m_DegreeRepositoryM.Create(degree);
            degree.Id = id;
            return degree;
        }

        public async Task<Degree> Get(string degreeId)
        {
            return await m_DegreeRepositoryM.Get(degreeId);
        }
        public async Task<bool> Update(Degree degree)
        {
            return await m_DegreeRepositoryM.Update(degree);
        }
        public async Task<bool> Delete(string id)
        {
            return await m_DegreeRepositoryM.Delete(id);
        }
        public async Task<List<Degree>> Search(string searchStr)
        {
            searchStr = searchStr.ToLower();
            return await m_DegreeRepositoryM.Search(searchStr);
        }

        public async Task<List<Degree>> Gets()
        {
            return await m_DegreeRepositoryM.Gets();
        }
    }
}
