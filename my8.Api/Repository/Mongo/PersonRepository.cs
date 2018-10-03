using Microsoft.Extensions.Options;
using MongoDB.Driver;
using my8.Api.Infrastructures;
using my8.Api.Interfaces.Mongo;
using my8.Api.Models;
using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Model = my8.Api.Models;

namespace my8.Api.Repository.Mongo
{
    public class PersonRepository : MongoRepositoryBase<Person>, IPersonRepository
    {
        IMongoCollection<Person> collection;
        FilterDefinition<Person> filter = FilterDefinition<Person>.Empty;
        public PersonRepository(IOptions<MongoConnection> mongoConnection) : base(mongoConnection)
        {
            collection = _db.GetCollection<Person>("Person");
        }
        public async Task<string> Create(Person Person)
        {
            try
            {
                await collection.InsertOneAsync(Person);
                return Person.PersonId;
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<Person> Get(string id)
        {
            filter = Builders<Person>.Filter.Eq(p => p.PersonId, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Person> GetByUrl(string url)
        {
            filter = Builders<Person>.Filter.Eq(p => p.Url, url);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Person>> SearchByDegrees(string[] keySearchs)
        {
            string formated = Utils.ArrStrToMongoSearch(keySearchs);
            return await collection.Find($@"{{'DegreesId':{{ '$in':[{formated}]}}}}").ToListAsync();
        }

        public async Task<List<Person>> SearchByExperience(int min, int max)
        {
            return await collection.Find($@"{{'$or':[{{'Experience':{{'$gte':{min}}}}},{{'Experience':{{'$lte':{max}}}}}]}}").ToListAsync();
        }

        //public async Task<List<Author>> SearchByEmploymentType(string keySearchs)
        //{
        //    return await shortPersonCollection.Find($@"{{'KeySearchs':{{ '$in':[{keySearchs}]}}}}").ToListAsync();
        //}

        public async Task<List<Person>> SearchByIndustries(string[] keySearchs)
        {
            string formated = Utils.ArrStrToMongoSearch(keySearchs);
            return await collection.Find($@"{{'IndustriesCode':{{ '$in':[{formated}]}}}}").ToListAsync();
        }

        public async Task<List<Person>> SearchByLocations(string[] keySearchs)
        {
            string formated = Utils.ArrStrToMongoSearch(keySearchs);
            return await collection.Find($@"{{'LocationId':{{ '$in':[{formated}]}}}}").ToListAsync();
        }

        //public Task<List<Person>> SearchBySeniorities(string[] keySearchs)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<List<Person>> SearchBySkills(string[] keySearchs)
        {
            string formated = Utils.ArrStrToMongoSearch(keySearchs);
            return await collection.Find($@"{{'SkillsCode':{{ '$in':[{formated}]}}}}").ToListAsync();
        }

        public async Task<bool> Update(Person person)
        {
            filter = Builders<Person>.Filter.Eq(p => p.PersonId, person.PersonId);
            var update = Builders<Person>.Update
                            .Set(s => s.DisplayName, person.DisplayName)
                            .Set(p => p.Rate, person.Rate)
                            .Set(p=>p.WorkAs,person.WorkAs)
                            .Set(p => p.Avatar, person.Avatar);
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
    }
}
