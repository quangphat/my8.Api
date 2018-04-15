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
    public class PostBroadcastPersonRepository:MongoRepositoryBase,IPostBroadcastPersonRepository
    {
		IMongoCollection<PostBroadcastPerson> collection;
		FilterDefinition<PostBroadcastPerson> filter = FilterDefinition<PostBroadcastPerson>.Empty;
        public PostBroadcastPersonRepository(IOptions<MongoConnection> setting) : base(setting) 
        {
            collection = _db.GetCollection<PostBroadcastPerson>("PostBroadcastPerson");
        }
        public async Task<bool> Create(PostBroadcastPerson postbroadcastperson)
        {
            try
            {
                await collection.InsertOneAsync(postbroadcastperson);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task<List<PostBroadcastPerson>> GetByPerson(string personId)
        {
            filter = Builders<PostBroadcastPerson>.Filter.Eq(p => p.PersonId, personId);
            List<PostBroadcastPerson> lstPost = await collection.Find(filter).ToListAsync();
            return lstPost;
        }
    }
}

