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
    public class SkillBusiness : ISkillBusiness
    {
        MongoI.ISkillRepository m_SkillRepositoryM;
        public SkillBusiness(MongoI.ISkillRepository skillRepoM)
        {
            m_SkillRepositoryM = skillRepoM;
        }
        public async Task<Skill> Create(Skill skill)
        {
            skill.Code = skill.Code.ToLower().Trim();
            string id = await m_SkillRepositoryM.Create(skill);
            skill.Id = id;
            return skill;
        }

        public async Task<Skill> Get(string skillId)
        {
            return await m_SkillRepositoryM.Get(skillId);
        }

        public async Task<Skill> GetByCode(string code)
        {
            return await m_SkillRepositoryM.GetByCode(code);
        }

        public async Task<List<Skill>> Gets()
        {
            return await m_SkillRepositoryM.Gets();
        }

        public async Task<bool> Update(Skill skill)
        {
            return await m_SkillRepositoryM.Update(skill);
        }
        public async Task<bool> Delete(string id)
        {
            return await m_SkillRepositoryM.Delete(id);
        }
        public async Task<List<Skill>> Search(string searchStr)
        {
            searchStr = searchStr.ToLower();
            return await m_SkillRepositoryM.Search(searchStr);
        }
    }
}
