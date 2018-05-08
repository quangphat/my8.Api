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
    public class AuthorTypeRepository:MongoRepositoryBase,IAuthorTypeRepository
    {
		IMongoCollection<AuthorType> collection;
		FilterDefinition<AuthorType> filter = FilterDefinition<AuthorType>.Empty;
        public AuthorTypeRepository(IOptions<MongoConnection> setting) : base(setting) 
        {
            collection = _db.GetCollection<AuthorType>("AuthorType");
        }
        public async Task<bool> Create(AuthorType authortype)
        {
            try
            {
                await collection.InsertOneAsync(authortype);
                return true;
            }
            catch { return false; }
        }

        public async Task<AuthorType> Get(string id)
        {
            filter = Builders<AuthorType>.Filter.Eq(p => p.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}

