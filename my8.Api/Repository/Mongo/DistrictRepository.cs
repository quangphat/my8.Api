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
    public class DistrictRepository:MongoRepositoryBase,IDistrictRepository
    {
		IMongoCollection<District> collection;
		FilterDefinition<District> filter = FilterDefinition<District>.Empty;
        public DistrictRepository(IOptions<MongoConnection> setting) : base(setting) 
        {
            collection = _db.GetCollection<District>("District");
        }
        public async Task<string> Create(District district)
        {
            try
            {
                await collection.InsertOneAsync(district);
                return district.Id;
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<District> Get(string id)
        {
            filter = Builders<District>.Filter.Eq(p => p.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<bool> Update(District district)
        {
            var filter = Builders<District>.Filter.Eq(p => p.Id, district.Id);
            var update = Builders<District>.Update
                            .Set(s => s.Id, district.Id);
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
                filter = Builders<District>.Filter.Eq(p => p.Id, id);
                await collection.DeleteOneAsync(filter);
                return true;
            }
            catch { return false; }
        }
        public async Task<List<District>> Search(string searchStr)
        {
            return await collection.Find($@"{{'KeySearchs':{{ '$in':[/{searchStr}/i]}}}}").ToListAsync();
        }
    }
}

