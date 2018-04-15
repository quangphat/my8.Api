using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface ISkillRepository
    {
        Task<string> Create(Skill skill);
        Task<Skill> Get(string id);
        Task<Skill> GetByCode(string code);
        Task<List<Skill>> Gets();
        Task<List<Skill>> Search(string searchStr);
        Task<bool> Update(Skill skill);
        Task<bool> Delete(string id);
    }
}

