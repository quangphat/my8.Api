using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface ILastPostBroadCastRepository
    {
        Task<string> Create(LastPostBroadCast lastpostbroadcast);
        Task<LastPostBroadCast> Get(string id);
        Task<LastPostBroadCast> GetByPageId(string pageId,string personId);
        Task<LastPostBroadCast> GetByCommunityId(string communityId,string personId);
        Task<bool> Update(LastPostBroadCast lastpostbroadcast);
        Task<bool> Delete(string id);
    }
}

