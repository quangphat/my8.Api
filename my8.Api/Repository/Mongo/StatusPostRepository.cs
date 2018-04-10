using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using my8.Api.Infrastructures;
using my8.Api.Interfaces.Mongo;
using my8.Api.Models.Mongo;
using my8.Api.Repository.Mongo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace my8.Api.Repository.Mongo
{
    public class StatusPostRepository : MongoRepositoryBase, IStatusPostRepository
    {
        IMongoCollection<StatusPost> collection;

        FilterDefinition<StatusPost> filter = FilterDefinition<StatusPost>.Empty;
        public StatusPostRepository(IOptions<MongoConnection> mongoConnection) : base(mongoConnection)
        {
            collection = _db.GetCollection<StatusPost>("StatusPost");
        }

        public async Task<StatusPost> GetByPostId(string id)
        {
            var filter = Builders<StatusPost>.Filter.Eq(p => p.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }


        public async Task<bool> UpdatePost(StatusPost post)
        {
            var filter = Builders<StatusPost>.Filter.Eq(p => p.Id, post.Id);
            var update = Builders<StatusPost>.Update
                            .Set(s => s.Content, post.Content)
                            .Set(p => p.EditedTime, DateTime.UtcNow)
                            .Set(p => p.Images, post.Images);
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

        public async Task<bool> UpdateShares(StatusPost post)
        {
            var filter = Builders<StatusPost>.Filter.Eq(p => p.Id, post.Id);
                
            var update = Builders<StatusPost>.Update
                            .Set(s => s.Shares, post.Shares);
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

        public async Task<bool> UpdateViews(StatusPost post)
        {
            var filter = Builders<StatusPost>.Filter.Eq(p => p.Id, post.Id);
            var update = Builders<StatusPost>.Update
                            .Set(s => s.Views, post.Views);
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
        public async Task<bool> UpdateComment(StatusPost post)
        {
            var filter = Builders<StatusPost>.Filter.Eq(p => p.Id, post.Id);
            var update = Builders<StatusPost>.Update
                            .Set(s => s.Comments, post.Comments);
            try
            {
                await collection.UpdateOneAsync(filter, update);
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> UpdateLikes(StatusPost post)
        {
            var filter = Builders<StatusPost>.Filter.Eq(p => p.Id, post.Id);
            var update = Builders<StatusPost>.Update
                            .Set(s => s.Likes, post.Likes);
            try
            {
                await collection.UpdateOneAsync(filter, update);
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> Post(StatusPost post)
        {
            try
            {
                await collection.InsertOneAsync(post);
                return true;
            }
            catch { return false; }
        }

        public async Task<List<StatusPost>> Gets(string[] id)
        {
            string[] ids = new string[id.Length];
            for (int i = 0; i < id.Length; i++)
            {
                string line = $"ObjectId('{id[i]}')";
                ids[i] = line;
            }
            string temp = String.Join(",", ids);
            List<StatusPost> statusPosts = await collection.Find("{ _id:{$in:[" + temp + "]}}").ToListAsync();
            return statusPosts;
        }

        public async Task<List<StatusPost>> GetByAuthor(Actor actor)
        {
            var filterBuilder = Builders<StatusPost>.Filter;
            filter = filterBuilder.Eq(p => p.PostBy.ActorId, actor.ActorId) & filterBuilder.Eq(p=>p.PostBy.ActorTypeId,actor.ActorTypeId);
            List<StatusPost> statusPosts = await collection.Find(filter).ToListAsync();
            return statusPosts;
        }

        public async Task<bool> Active(string postId,bool active)
        {
            var filter = Builders<StatusPost>.Filter.Eq(p => p.Id, postId);
            var update = Builders<StatusPost>.Update
                            .Set(s => s.Active, active);
            try
            {
                await collection.UpdateOneAsync(filter, update);
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> DeletePost(string postId)
        {
            var filter = Builders<StatusPost>.Filter.Eq(p => p.Id, postId);
            try
            {
                await collection.DeleteOneAsync(filter);
                return true;
            }
            catch { return false; }
        }

    }
}
