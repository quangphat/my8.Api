using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface ILastPostBroadCastBusiness
    {
        Task<LastPostBroadCast> Create(LastPostBroadCast lastpostbroadcast);
        Task<LastPostBroadCast> Get(string lastpostbroadcastId);
        Task<bool> Update(LastPostBroadCast lastpostbroadcast);
        Task<bool> Delete(string id);
    }
}
