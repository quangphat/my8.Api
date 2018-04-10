
using my8.Api.Models.Neo4j;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Neo4j
{
    public interface IClubRepository
    {
        Task<IEnumerable<Person>> GetMembers(Club team);
        Task<int> CountMember(Club team);
        Task KickOutMember(Club team, Person user);
    }
}
