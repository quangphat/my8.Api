using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface IExperienceBusiness
    {
        Task<Experience> Create(Experience experience);
        Task<Experience> Get(string experienceId);
        Task<Pagination<Experience>> GetByPersonId(string personId, int page, int limit);
        Task<bool> Update(Experience experience);
        Task<bool> Delete(string id);
        Task<List<Experience>> Search(string searchStr);
    }
}
