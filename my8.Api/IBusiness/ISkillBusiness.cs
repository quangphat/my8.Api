using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface ISkillBusiness
    {
        Task<Skill> Create(Skill skill);
        Task<Skill> Get(string skillId);
        Task<Skill> GetByCode(string code);
        Task<List<Skill>> Gets();
        Task<bool> Update(Skill skill);
        Task<bool> Delete(string id);
        Task<List<Skill>> Search(string searchStr);
    }
}
