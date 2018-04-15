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
    public class ProvinceRepository:MongoRepositoryBase,IProvinceRepository
    {
		IMongoCollection<Province> collection;
		FilterDefinition<Province> filter = FilterDefinition<Province>.Empty;
        public ProvinceRepository(IOptions<MongoConnection> setting) : base(setting) 
        {
            collection = _db.GetCollection<Province>("Province");
        }
        public async Task<string> Create(Province province)
        {
            try
            {
                await collection.InsertOneAsync(province);
                return province.Id;
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<Province> Get(string id)
        {
            filter = Builders<Province>.Filter.Eq(p => p.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<bool> Update(Province province)
        {
            var filter = Builders<Province>.Filter.Eq(p => p.Id, province.Id);
            var update = Builders<Province>.Update
                            .Set(s => s.Id, province.Id);
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
                filter = Builders<Province>.Filter.Eq(p => p.Id, id);
                await collection.DeleteOneAsync(filter);
                return true;
            }
            catch { return false; }
        }
        public async Task<List<Province>> Search(string searchStr)
        {
            return null;
        }
    }
}

