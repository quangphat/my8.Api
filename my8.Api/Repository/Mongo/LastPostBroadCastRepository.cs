using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using my8.Api.Infrastructures;
using my8.Api.Interfaces.Mongo;
using my8.Api.Models;
using my8.Api.my8Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Repository.Mongo
{
    public class LastPostBroadCastRepository : MongoRepositoryBase, ILastPostBroadCastRepository
    {
        IMongoCollection<LastPostBroadCast> collection;
        FilterDefinition<LastPostBroadCast> filter = FilterDefinition<LastPostBroadCast>.Empty;
        public LastPostBroadCastRepository(IOptions<MongoConnection> setting) : base(setting)
        {
            collection = _db.GetCollection<LastPostBroadCast>("LastPostBroadCast");
        }
        public async Task<string> Create(LastPostBroadCast lastpostbroadcast)
        {
            try
            {
                await collection.InsertOneAsync(lastpostbroadcast);
                return lastpostbroadcast.Id;
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<LastPostBroadCast> Get(string id)
        {
            filter = Builders<LastPostBroadCast>.Filter.Eq(p => p.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<bool> Update(LastPostBroadCast lastpostbroadcast)
        {
            var filter = Builders<LastPostBroadCast>.Filter.Eq(p => p.Id, lastpostbroadcast.Id);
            var update = Builders<LastPostBroadCast>.Update
                            .Set(s => s.Id, lastpostbroadcast.Id)
                            .Set(s => s.AuthorId, lastpostbroadcast.AuthorId)
                            .Set(s => s.AuthorType, lastpostbroadcast.AuthorType)
                            .Set(s => s.PersonId, lastpostbroadcast.PersonId)
                            .Set(s => s.LastPostIdToPerson, lastpostbroadcast.LastPostIdToPerson)
                            .Set(s => s.LastPostTimeToPerson, lastpostbroadcast.LastPostTimeToPerson);
            try
            {
                await collection.UpdateOneAsync(filter, update);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Delete(string id)
        {
            try
            {
                filter = Builders<LastPostBroadCast>.Filter.Eq(p => p.Id, id);
                await collection.DeleteOneAsync(filter);
                return true;
            }
            catch { return false; }
        }

        public async Task<LastPostBroadCast> GetByPageId(string pageId,string personId)
        {
            try
            {
                return await collection.Find($@"{{'AuthorId':'{pageId}','AuthorType':{(int)AuthorType.Page},PersonId:'{personId}'}}").FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<LastPostBroadCast> GetByCommunityId(string communityId, string personId)
        {
            try
            {
                return await collection.Find($@"{{'AuthorId':'{communityId}','AuthorType':{(int)AuthorType.Community},PersonId:'{personId}'}}").FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}

