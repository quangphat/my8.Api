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
    public class ExperienceRepository : MongoRepositoryBase<Experience>, IExperienceRepository
    {
        IMongoCollection<Experience> collection;
        FilterDefinition<Experience> filter = FilterDefinition<Experience>.Empty;
        public ExperienceRepository(IOptions<MongoConnection> setting) : base(setting)
        {
            collection = _db.GetCollection<Experience>("Experience");
        }
        public async Task<string> Create(Experience experience)
        {
            try
            {
                await collection.InsertOneAsync(experience);
                return experience.Id;
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<Experience> Get(string id)
        {
            filter = Builders<Experience>.Filter.Eq(p => p.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<bool> Update(Experience experience)
        {
            var filter = Builders<Experience>.Filter.Eq(p => p.Id, experience.Id);
            var update = Builders<Experience>.Update
                            .Set(s => s.Title, experience.Title)
                            .Set(s => s.CompanyName, experience.CompanyName)
                            .Set(s => s.Location, experience.Location)
                            .Set(s => s.FromDate, experience.FromDate)
                            .Set(s => s.isCurrentlyWorkHere, experience.isCurrentlyWorkHere)
                            .Set(s => s.ToDate, experience.ToDate)
                            .Set(s => s.Industry, experience.Industry)
                            .Set(s => s.WorkAs, experience.WorkAs)
                            .Set(s => s.Description, experience.Description)
                            .Set(s => s.PersonId, experience.PersonId)
                            .Set(s => s.CreatedTime, experience.CreatedTime)
                            .Set(s => s.UpdatedTime, experience.UpdatedTime);
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
                filter = Builders<Experience>.Filter.Eq(p => p.Id, id);
                await collection.DeleteOneAsync(filter);
                return true;
            }
            catch { return false; }
        }
        public async Task<List<Experience>> Search(string searchStr)
        {
            return null;
        }

        public async Task<Pagination<Experience>> GetByPersonId(string personId,int page,int limit)
        {
            var response = await GetPaginationAsync(collection, page, limit, null, null);
            Pagination<Experience> result = new Pagination<Experience> {
                TotalRecord = response.total,
                Datas = response.datas.ToList()
            };
            return result;
            //return await collection.Find($@"{{'PersonId':'{personId}'}}").Sort("{FromDateUnix:-1}").Skip(page*limit).Limit(limit).ToListAsync();
        }
    }
}

