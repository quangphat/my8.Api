using my8.Api.Models.Mongo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IUniversityRepository
    {
        Task<bool> Create(University university);
        Task<University> Get(string id);
    }
}

