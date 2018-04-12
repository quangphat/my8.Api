using my8.Api.Models;
using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface ITeamRepository
    {
        Task<string> Create(Team team);
        Task<Team> Get(string id);
    }
}

