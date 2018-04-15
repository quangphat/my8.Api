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
    public class SkillRepository:MongoRepositoryBase,ISkillRepository
    {
		IMongoCollection<Skill> collection;
		FilterDefinition<Skill> filter = FilterDefinition<Skill>.Empty;
        public SkillRepository(IOptions<MongoConnection> setting) : base(setting) 
        {
            collection = _db.GetCollection<Skill>("Skill");
        }
        public async Task<string> Create(Skill skill)
        {
            try
            {
                await collection.InsertOneAsync(skill);
                return skill.Id;
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<Skill> Get(string id)
        {
            filter = Builders<Skill>.Filter.Eq(p => p.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Skill> GetByCode(string code)
        {
            filter = Builders<Skill>.Filter.Eq(p => p.Code, code);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Skill>> Gets()
        {
            filter = FilterDefinition<Skill>.Empty;
            return await collection.Find(filter).ToListAsync();
        }

        public async Task<bool> Update(Skill skill)
        {
            var filter = Builders<Skill>.Filter.Eq(p => p.Id, skill.Id);
            var update = Builders<Skill>.Update
                            .Set(s => s.Id, skill.Id)
                            .Set(s => s.Code, skill.Code)
                            .Set(s => s.Display, skill.Display);
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
                filter = Builders<Skill>.Filter.Eq(p => p.Id, id);
                await collection.DeleteOneAsync(filter);
                return true;
            }
            catch { return false; }
        }

        public async Task<List<Skill>> Search(string searchStr)
        {
            return await collection.Find("{'$or':[{'Code':/" + searchStr + "/i},{'Display':/" + searchStr + "/i}]}").ToListAsync();
        }
    }
}

