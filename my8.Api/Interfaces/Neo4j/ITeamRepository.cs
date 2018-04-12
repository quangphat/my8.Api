using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Neo4j
{
    public interface ITeamRepository
    {
        Task<bool> Create(Team team);
        Task<Team> Get(string id);
    }
}
