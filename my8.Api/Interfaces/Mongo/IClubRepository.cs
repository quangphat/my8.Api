using my8.Api.Models.Mongo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IClubRepository
    {
        Task<bool> Create(Club club);
        Task<Club> Get(string id);
    }
}

