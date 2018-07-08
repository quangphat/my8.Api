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
    public class JobFunctionRepository : MongoRepositoryBase, IJobFunctionRepository
    {
        IMongoCollection<JobFunction> collection;
        FilterDefinition<JobFunction> filter = FilterDefinition<JobFunction>.Empty;
        public JobFunctionRepository(IOptions<MongoConnection> setting) : base(setting)
        {
            collection = _db.GetCollection<JobFunction>("JobFunction");
        }
        public async Task<string> Create(JobFunction JobFunction)
        {
            try
            {
                await collection.InsertOneAsync(JobFunction);
                return JobFunction.Id;
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<JobFunction> Get(string id)
        {
            filter = Builders<JobFunction>.Filter.Eq(p => p.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<JobFunction> GetbyCode(string code)
        {
            filter = Builders<JobFunction>.Filter.Eq(p => p.Code, code);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<JobFunction>> Gets()
        {
            filter = FilterDefinition<JobFunction>.Empty;
            return await collection.Find(filter).ToListAsync();
        }

        public Task<List<JobFunction>> GetsByPerson(string personId)
        {
            return null;
        }

        public async Task<bool> Update(JobFunction JobFunction)
        {
            var filter = Builders<JobFunction>.Filter.Eq(p => p.Id, JobFunction.Id);
            var update = Builders<JobFunction>.Update
                            .Set(s => s.Id, JobFunction.Id)
                            .Set(s => s.Code, JobFunction.Code)
                            .Set(s => s.KeySearchs, JobFunction.KeySearchs)
                            .Set(s => s.Display, JobFunction.Display);

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
                filter = Builders<JobFunction>.Filter.Eq(p => p.Id, id);
                await collection.DeleteOneAsync(filter);
                return true;
            }
            catch { return false; }
        }

        public async Task<List<JobFunction>> Search(string searchStr)
        {
            return await collection.Find($@"{{'KeySearchs':{{ '$in':[/{searchStr}/i]}}}}").ToListAsync();
        }
    }
}

