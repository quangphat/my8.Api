using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IClubRepository
    {
        Task<string> Create(Club club);
        Task<Club> Get(string id);
    }
}

