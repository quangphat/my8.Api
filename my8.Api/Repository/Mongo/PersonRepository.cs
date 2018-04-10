using Microsoft.Extensions.Options;
using MongoDB.Driver;
using my8.Api.Infrastructures;
using my8.Api.Interfaces.Mongo;
using my8.Api.Models.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Repository.Mongo
{
    public class PersonRepository : MongoRepositoryBase, IPersonRepository
    {
        IMongoCollection<Person> collection;

        FilterDefinition<Person> filter = FilterDefinition<Person>.Empty;
        public PersonRepository(IOptions<MongoConnection> mongoConnection) : base(mongoConnection)
        {
            collection = _db.GetCollection<Person>("Person");
        }
        public async Task<bool> Create(Person Person)
        {
            try
            {
                await collection.InsertOneAsync(Person);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Person> Get(string id)
        {
            filter = Builders<Person>.Filter.Eq(p => p.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Person person)
        {
            filter = Builders<Person>.Filter.Eq(p => p.Id, person.Id);
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
