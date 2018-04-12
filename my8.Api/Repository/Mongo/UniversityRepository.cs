using Microsoft.Extensions.Options;
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
    public class UniversityRepository:MongoRepositoryBase,IUniversityRepository
    {
		IMongoCollection<University> collection;
        public UniversityRepository(IOptions<MongoConnection> setting) : base(setting) 
        {
            collection = _db.GetCollection<University>("University");
        }
        public async Task<bool> Create(University university)
        {
            throw new NotImplementedException();
        }

        public async Task<University> Get(string id)
        {
            throw new NotImplementedException();
        }
    }
}

