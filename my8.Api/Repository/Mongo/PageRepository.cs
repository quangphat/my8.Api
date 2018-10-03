using AutoMapper;
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
    public class PageRepository:MongoRepositoryBase<Page>,IPageRepository
    {
		IMongoCollection<Page> collection;
		FilterDefinition<Page> filter = FilterDefinition<Page>.Empty;
        public PageRepository(IOptions<MongoConnection> setting) : base(setting) 
        {
            collection = _db.GetCollection<Page>("Page");
        }
        public async Task<string> Create(Page page)
        {
            try
            {
                await collection.InsertOneAsync(page);
                return page.PageId;
            }
            catch { return string.Empty; }
        }
        public async Task<bool> Update(Page page)
        {
            filter = Builders<Page>.Filter.Eq(p => p.PageId, page.PageId);
            var update = Builders<Page>.Update
                            .Set(p => p.DisplayName, page.DisplayName)
                            .Set(p => p.Rate, page.Rate)
                            .Set(p => p.Title, page.Title)
                            .Set(p => p.Avatar, page.Avatar)
                            .Set(p => p.PageIPoint, page.PageIPoint);
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
        public async Task<Page> Get(string id)
        {
            filter = Builders<Page>.Filter.Eq(p => p.PageId, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        
    }
}

