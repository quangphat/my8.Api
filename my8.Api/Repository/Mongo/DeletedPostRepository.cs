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
    public class DeletedPostRepository:MongoRepositoryBase<DeletedPost>,IDeletedPostRepository
    {
		IMongoCollection<DeletedPost> collection;
		FilterDefinition<DeletedPost> filter = FilterDefinition<DeletedPost>.Empty;
        public DeletedPostRepository(IOptions<MongoConnection> setting) : base(setting) 
        {
            collection = _db.GetCollection<DeletedPost>("DeletedPost");
        }
        public async Task<bool> AddPostToDeleted(DeletedPost deletedpost)
        {
            try
            {
                await collection.InsertOneAsync(deletedpost);
                return true;
            }
            catch { return false; }
        }
    }
}

