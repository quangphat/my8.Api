using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface ICommunityBusiness
    {
        Task<Community> Create(Community Community);
        Task<bool> Update(Community Community);
        Task<CommunityAllin> Get(string id);
        Task<List<PersonAllin>> GetMembers(string CommunityId);
        Task<List<CommunityAllin>> Search(string searchStr, int skip, int limit);
        Task<bool> AddMember(string CommunityId, string personId);
        Task<bool> KickOutMember(string CommunityId, string personId);
    }

}
