using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface ICommunityRepository
    {
        Task<string> Create(Community Community);
        Task<Community> Get(string id);
    }
}

