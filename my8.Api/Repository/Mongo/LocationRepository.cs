using Microsoft.Extensions.Options;
using MongoDB.Bson;
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
    public class LocationRepository:MongoRepositoryBase,ILocationRepository
    {
		IMongoCollection<Location> collection;
		FilterDefinition<Location> filter = FilterDefinition<Location>.Empty;
        public LocationRepository(IOptions<MongoConnection> setting) : base(setting) 
        {
            collection = _db.GetCollection<Location>("Location");
        }
        public async Task<string> Create(Location location)
        {
            try
            {
                await collection.InsertOneAsync(location);
                return location.Id;
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<Location> Get(string id)
        {
            filter = Builders<Location>.Filter.Eq(p => p.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<bool> Update(Location location)
        {
            var filter = Builders<Location>.Filter.Eq(p => p.Id, location.Id);
            var update = Builders<Location>.Update
                            .Set(s => s.Id, location.Id)
                            .Set(s => s.Country, location.Country)
                            .Set(s => s.Province, location.Province)
                            .Set(s => s.District, location.District)
                            .Set(s => s.Street, location.Street)
                            .Set(s => s.Display, location.Display);


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
                filter = Builders<Location>.Filter.Eq(p => p.Id, id);
                await collection.DeleteOneAsync(filter);
                return true;
            }
            catch { return false; }
        }
        public async Task<List<Location>> Search(string searchStr)
        {
            return await collection.Find("{'$or':[{'Country.Name':/" + searchStr + "/i},{'Province.Name':/" + searchStr + "/i},{'District.Name':/" + searchStr + "/i},{'Street':/" + searchStr + "/i}]}").ToListAsync();
        }
    }
}

