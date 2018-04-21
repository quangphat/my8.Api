using Microsoft.Extensions.Options;
using MongoDB.Driver;
using my8.Api.Infrastructures;
using my8.Api.Interfaces.Mongo;
using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Repository.Mongo
{
    public class CommunityRepository:MongoRepositoryBase,ICommunityRepository
    {
		IMongoCollection<Community> collection;
		FilterDefinition<Community> filter = FilterDefinition<Community>.Empty;
        public CommunityRepository(IOptions<MongoConnection> setting) : base(setting) 
        {
            collection = _db.GetCollection<Community>("Community");
        }
        public async Task<string> Create(Community Community)
        {
            try
            {
                await collection.InsertOneAsync(Community);
                return Community.CommunityId;
            }
            catch { return string.Empty; }
        }

        public async Task<Community> Get(string id)
        {
            filter = Builders<Community>.Filter.Eq(p => p.CommunityId, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}

