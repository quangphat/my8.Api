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
    public class StatusPostRepository : MongoRepositoryBase<StatusPost>, IStatusPostRepository
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
        public async Task<List<StatusPost>> GetByAuthorPerson(string personId, int page, int limit, long lastPostTimeUnix=0)
        {
            return await collection.Find($@"{{'PersonId':'{personId}',PostTimeUnix:{{$gt:{lastPostTimeUnix}}}}}").Sort("{PostTimeUnix:-1}").Skip(page * limit).Limit(limit).ToListAsync();
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
                            .Set(s => s.EditedTimeUnix, post.EditedTimeUnix)
                            .Set(s => s.EditedBy, post.EditedBy)
                            .Set(s => s.PersonId, post.PersonId)
                            .Set(s => s.PostingAsType, post.PostingAsType)
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

        public async Task<bool> UpdateShares(string postId, bool inc)
        {
            try
            {
                if (inc)
                {
                    await collection.UpdateOneAsync($@"{{_id:ObjectId('{postId}')}}", "{$inc:{Shares:1}}");
                }
                else
                {
                    await collection.UpdateOneAsync($@"{{_id:ObjectId('{postId}'),Shares:{{$gt:0}}}}", "{$inc:{Shares:-1}}");
                }
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> UpdateViews(string postId, bool inc)
        {
            try
            {
                if (inc)
                {
                    await collection.UpdateOneAsync($@"{{_id:ObjectId('{postId}')}}", "{$inc:{Views:1}}");
                }
                else
                {
                    await collection.UpdateOneAsync($@"{{_id:ObjectId('{postId}'),Views:{{$gt:0}}}}", "{$inc:{Views:-1}}");
                }
                return true;
            }
            catch { return false; }
        }
        public async Task<bool> UpdateComments(string postId,bool inc)
        {
            try
            {
                if(inc)
                {
                    await collection.UpdateOneAsync($@"{{_id:ObjectId('{postId}')}}", "{$inc:{Comments:1}}");
                }
                else
                {
                    await collection.UpdateOneAsync($@"{{_id:ObjectId('{postId}'),Comments:{{$gt:0}}}}", "{$inc:{Comments:-1}}");
                }
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> Like(string postId)
        {
            try
            {
                await collection.UpdateOneAsync($@"{{_id:ObjectId('{postId}')}}", "{$inc:{Likes:1}}");
                return true;
            }
            catch { return false; }
        }
        public async Task<bool> UnLike(string postId)
        {
            try
            {
                await collection.UpdateOneAsync($@"{{_id:ObjectId('{postId}'),Likes:{{$gt:0}}}}", "{$inc:{Likes:-1}}");
                return true;
            }
            catch { return false; }
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

        public Task<List<StatusPost>> GetByAuthorPerson(string personId, int page, int limit)
        {
            throw new NotImplementedException();
        }
    }
}
