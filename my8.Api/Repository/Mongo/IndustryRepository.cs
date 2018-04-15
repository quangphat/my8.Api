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
    public class IndustryRepository : MongoRepositoryBase, IIndustryRepository
    {
        IMongoCollection<Industry> collection;
        FilterDefinition<Industry> filter = FilterDefinition<Industry>.Empty;
        public IndustryRepository(IOptions<MongoConnection> setting) : base(setting)
        {
            collection = _db.GetCollection<Industry>("Industry");
        }
        public async Task<string> Create(Industry industry)
        {
            try
            {
                await collection.InsertOneAsync(industry);
                return industry.Id;
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<Industry> Get(string id)
        {
            filter = Builders<Industry>.Filter.Eq(p => p.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Industry> GetbyCode(string code)
        {
            filter = Builders<Industry>.Filter.Eq(p => p.Code, code);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Industry>> Gets()
        {
            filter = FilterDefinition<Industry>.Empty;
            return await collection.Find(filter).ToListAsync();
        }

        public Task<List<Industry>> GetsByPerson(string personId)
        {
            return null;
        }

        public async Task<bool> Update(Industry industry)
        {
            var filter = Builders<Industry>.Filter.Eq(p => p.Id, industry.Id);
            var update = Builders<Industry>.Update
                            .Set(s => s.Id, industry.Id)
                            .Set(s => s.Code, industry.Code)
                            .Set(s => s.Display, industry.Display);

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
                filter = Builders<Industry>.Filter.Eq(p => p.Id, id);
                await collection.DeleteOneAsync(filter);
                return true;
            }
            catch { return false; }
        }

        public async Task<List<Industry>> Search(string searchStr)
        {
            return await collection.Find("{'$or':[{'Code':/" + searchStr + "/i},{'Display':/" + searchStr + "/i}]}").ToListAsync();
        }
    }
}

