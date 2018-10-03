using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IExperienceRepository
    {
        Task<string> Create(Experience experience);
        Task<Experience> Get(string id);
        Task<Pagination<Experience>> GetByPersonId(string personId, int page, int limit);
        Task<bool> Update(Experience experience);
        Task<bool> Delete(string id);
        Task<List<Experience>> Search(string searchStr);
    }
}

