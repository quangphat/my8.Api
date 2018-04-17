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
    public class SeniorityLevelRepository:MongoRepositoryBase,ISeniorityLevelRepository
    {
		IMongoCollection<SeniorityLevel> collection;
		FilterDefinition<SeniorityLevel> filter = FilterDefinition<SeniorityLevel>.Empty;
        public SeniorityLevelRepository(IOptions<MongoConnection> setting) : base(setting) 
        {
            collection = _db.GetCollection<SeniorityLevel>("SeniorityLevel");
        }
        public async Task<string> Create(SeniorityLevel senioritylevel)
        {
            try
            {
                await collection.InsertOneAsync(senioritylevel);
                return senioritylevel.Id;
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<SeniorityLevel> Get(string id)
        {
            filter = Builders<SeniorityLevel>.Filter.Eq(p => p.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<bool> Update(SeniorityLevel senioritylevel)
        {
            var filter = Builders<SeniorityLevel>.Filter.Eq(p => p.Id, senioritylevel.Id);
            var update = Builders<SeniorityLevel>.Update
                            .Set(s => s.Id, senioritylevel.Id);
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
                filter = Builders<SeniorityLevel>.Filter.Eq(p => p.Id, id);
                await collection.DeleteOneAsync(filter);
                return true;
            }
            catch { return false; }
        }
        public async Task<List<SeniorityLevel>> Search(string searchStr)
        {
            return await collection.Find($@"{{'$or':[{{'Name':/{searchStr}/i}}]}}").ToListAsync();
        }

        public async Task<List<SeniorityLevel>> Gets()
        {
            filter = FilterDefinition<SeniorityLevel>.Empty;
            return await collection.Find(filter).ToListAsync();
        }
    }
}

