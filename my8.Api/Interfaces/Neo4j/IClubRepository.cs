
using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Neo4j
{
    public interface IClubRepository
    {
        Task<bool> Create(Club club);
        Task<bool> Update(Club club);
        Task<ClubAllin> Get(string clubId);
        Task<IEnumerable<PersonAllin>> GetMembers(string clubId);
        Task<bool> AddMember(string clubId, string personId);
        Task<bool> KickOutMember(string clubId, string personId);
        Task<IEnumerable<ClubAllin>> Search(string searchStr, int skip, int limit);
    }
}
