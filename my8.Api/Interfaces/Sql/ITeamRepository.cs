using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace my8.Api.Interfaces.Sql
{
    public interface ITeamRepository
    {
        Task<bool> Create(Team team);
        Task<Team> Get(string id);
        Task<bool> Update(Team team);
    }
}

