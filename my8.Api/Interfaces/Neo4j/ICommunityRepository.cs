
using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Neo4j
{
    public interface ICommunityRepository
    {
        Task<bool> Create(Community Community);
        Task<bool> Update(Community Community);
        Task<CommunityAllin> Get(string CommunityId);
        Task<IEnumerable<PersonAllin>> GetMembers(string CommunityId);
        Task<bool> AddMember(string CommunityId, string personId);
        Task<bool> KickOutMember(string CommunityId, string personId);
        Task<IEnumerable<CommunityAllin>> Search(string searchStr, int skip, int limit);
    }
}
