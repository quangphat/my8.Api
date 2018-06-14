using my8.Api.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoI = my8.Api.Interfaces.Mongo;
using NeoI = my8.Api.Interfaces.Neo4j;
using my8.Api.Models;
using AutoMapper;
namespace my8.Api.Business
{
    public class LastPostBroadCastBusiness : ILastPostBroadCastBusiness
    {
        MongoI.ILastPostBroadCastRepository m_LastPostBroadCastRepositoryM;
        public LastPostBroadCastBusiness(MongoI.ILastPostBroadCastRepository lastpostbroadcastRepoM)
        {
            m_LastPostBroadCastRepositoryM = lastpostbroadcastRepoM;
        }
        public async Task<LastPostBroadCast> Create(LastPostBroadCast lastpostbroadcast)
        {
            string id = await m_LastPostBroadCastRepositoryM.Create(lastpostbroadcast);
            lastpostbroadcast.Id = id;
            return lastpostbroadcast;
        }

        public async Task<LastPostBroadCast> Get(string lastpostbroadcastId)
        {
            return await m_LastPostBroadCastRepositoryM.Get(lastpostbroadcastId);
        }
        public async Task<bool> Update(LastPostBroadCast lastpostbroadcast)
        {
            return await m_LastPostBroadCastRepositoryM.Update(lastpostbroadcast);
        }
        public async Task<bool> Delete(string id)
        {
            return await m_LastPostBroadCastRepositoryM.Delete(id);
        }
    }
}
