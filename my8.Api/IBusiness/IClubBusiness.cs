using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface IClubBusiness
    {
        Task<Club> Create(Club club);
        Task<bool> Update(Club club);
        Task<ClubAllin> Get(string id);
        Task<List<PersonAllin>> GetMembers(string clubId);
        Task<List<ClubAllin>> Search(string searchStr, int skip, int limit);
        Task<bool> AddMember(string clubId, string personId);
        Task<bool> KickOutMember(string clubId, string personId);
    }

}
