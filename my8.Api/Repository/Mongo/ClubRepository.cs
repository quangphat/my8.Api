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
    public class ClubRepository:MongoRepositoryBase,IClubRepository
    {
		IMongoCollection<Club> collection;
		FilterDefinition<Club> filter = FilterDefinition<Club>.Empty;
        public ClubRepository(IOptions<MongoConnection> setting) : base(setting) 
        {
            collection = _db.GetCollection<Club>("Club");
        }
        public async Task<bool> Create(Club club)
        {
            try
            {
                await collection.InsertOneAsync(club);
                return true;
            }
            catch { return false; }
        }

        public async Task<Club> Get(string id)
        {
            filter = Builders<Club>.Filter.Eq(p => p.ClubId, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}

