using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IDegreeRepository
    {
        Task<string> Create(Degree degree);
        Task<Degree> Get(string id);
        Task<List<Degree>> Gets();
        Task<bool> Update(Degree degree);
        Task<bool> Delete(string id);
        Task<List<Degree>> Search(string searchStr);
    }
}

