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
    public class DegreeRepository:MongoRepositoryBase,IDegreeRepository
    {
		IMongoCollection<Degree> collection;
		FilterDefinition<Degree> filter = FilterDefinition<Degree>.Empty;
        public DegreeRepository(IOptions<MongoConnection> setting) : base(setting) 
        {
            collection = _db.GetCollection<Degree>("Degree");
        }
        public async Task<string> Create(Degree degree)
        {
            try
            {
                await collection.InsertOneAsync(degree);
                return degree.Id;
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<Degree> Get(string id)
        {
            filter = Builders<Degree>.Filter.Eq(p => p.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<bool> Update(Degree degree)
        {
            var filter = Builders<Degree>.Filter.Eq(p => p.Id, degree.Id);
            var update = Builders<Degree>.Update
                            .Set(s => s.Id, degree.Id)
                            .Set(s=>s.Value,degree.Value)
                            .Set(s=>s.Name,degree.Name);
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
                filter = Builders<Degree>.Filter.Eq(p => p.Id, id);
                await collection.DeleteOneAsync(filter);
                return true;
            }
            catch { return false; }
        }
        public async Task<List<Degree>> Search(string searchStr)
        {
            return await collection.Find("{'$or':[{'Name':/" + searchStr + "/i}]}").ToListAsync();
        }

        public async Task<List<Degree>> Gets()
        {
            filter = FilterDefinition<Degree>.Empty;
            return await collection.Find(filter).ToListAsync();
        }
    }
}

