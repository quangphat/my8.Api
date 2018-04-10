using Microsoft.Extensions.Options;
using MongoDB.Driver;
using my8.Api.Infrastructures;
using my8.Api.Interfaces.Mongo;
using my8.Api.Models.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Repository.Mongo
{
    public class ActorTypeRepository:MongoRepositoryBase,IActorTypeRepository
    {
		IMongoCollection<ActorType> collection;
		FilterDefinition<ActorType> filter = FilterDefinition<ActorType>.Empty;
        public ActorTypeRepository(IOptions<MongoConnection> setting) : base(setting) 
        {
            collection = _db.GetCollection<ActorType>("ActorType");
        }
        public async Task<bool> Create(ActorType actortype)
        {
            try
            {
                await collection.InsertOneAsync(actortype);
                return true;
            }
            catch { return false; }
        }

        public async Task<ActorType> Get(string id)
        {
            filter = Builders<ActorType>.Filter.Eq(p => p.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}

