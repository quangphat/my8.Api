using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using my8.Api.Infrastructures;
using my8.Api.Interfaces.Mongo;
using my8.Api.Models;
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
        public async Task<string> Post(StatusPost post)
        {
            try
            {
                await collection.InsertOneAsync(post);
                return post.Id;
            }
            catch { return string.Empty; }
        }
        public async Task<StatusPost> Get(string id)
        {
            var filter = Builders<StatusPost>.Filter.Eq(p => p.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<List<StatusPost>> Gets(string[] id)
        {
            string temp = Utils.ArrStrIdToMongoDbId(id);
            List<StatusPost> statusPosts = await collection.Find($@"{{ _id:{{$in:[{temp}]}}}}").ToListAsync();
            return statusPosts;
        }

        public async Task<bool> UpdatePost(StatusPost post)
        {
            var filter = Builders<StatusPost>.Filter.Eq(p => p.Id, post.Id);
            var update = Builders<StatusPost>.Update
                            .Set(s => s.Content, post.Content)
                            .Set(s => s.EditedTime, post.EditedTime)
                            .Set(s => s.Likes, post.Likes)
                            .Set(s => s.Comments, post.Comments)
                            .Set(s => s.Shares, post.Shares)
                            .Set(s => s.Views, post.Views)
                            .Set(s => s.Images, post.Images)
                            .Set(s => s.PersonTags, post.PersonTags)
                            .Set(s => s.IsShareExperience, post.IsShareExperience)
                            .Set(s => s.IsAds, post.IsAds);

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
        public async Task<bool> UpdateComments(string postId)
        {
            var filter = Builders<StatusPost>.Filter.Eq(p => p.Id, postId);
            StatusPost post = await Get(postId);
            if (post == null) return false;
            var update = Builders<StatusPost>.Update
                            .Set(s => s.Comments, post.Comments+1);
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


        public async Task<List<StatusPost>> GetByAuthor(ShortPerson author)
        {
            var filterBuilder = Builders<StatusPost>.Filter;
            filter = filterBuilder.Eq(p => p.PostBy.AuthorId, author.AuthorId) & filterBuilder.Eq(p => p.PostBy.AuthorTypeId, author.AuthorTypeId);
            List<StatusPost> statusPosts = await collection.Find(filter).ToListAsync();
            return statusPosts;
        }

        public async Task<bool> Active(string postId, bool active)
        {
            //var filter = Builders<StatusPost>.Filter.Eq(p => p.Id, postId);
            //var update = Builders<StatusPost>.Update
            //                .Set(s => s.Active, active);
            //try
            //{
            //    await collection.UpdateOneAsync(filter, update);
            //    return true;
            //}
            //catch { return false; }
            return false;
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
