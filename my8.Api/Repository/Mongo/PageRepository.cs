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
    public class PageRepository:MongoRepositoryBase,IPageRepository
    {
		IMongoCollection<Page> collection;
		FilterDefinition<Page> filter = FilterDefinition<Page>.Empty;
        public PageRepository(IOptions<MongoConnection> setting) : base(setting) 
        {
            collection = _db.GetCollection<Page>("Page");
        }
        public async Task<bool> Create(Page page)
        {
            try
            {
                await collection.InsertOneAsync(page);
                return true;
            }
            catch { return false; }
        }

        public async Task<Page> Get(string id)
        {
            filter = Builders<Page>.Filter.Eq(p => p.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}

