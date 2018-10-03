using Microsoft.Extensions.Options;
using MongoDB.Driver;
using my8.Api.Infrastructures;
using my8.Api.Interfaces.Mongo;
using my8.Api.Models;
using my8.Api.my8Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Repository.Mongo
{
    public class CommentRepository : MongoRepositoryBase<Comment>,ICommentRepository
    {
        IMongoCollection<Comment> collection;
        FilterDefinition<Comment> filter = FilterDefinition<Comment>.Empty;
        public CommentRepository(IOptions<MongoConnection> mongoConnection) : base(mongoConnection)
        {
            collection = _db.GetCollection<Comment>("Comment");
        }

        public async Task<string> Create(Comment comment)
        {
            try
            {
                await collection.InsertOneAsync(comment);
                return comment.Id;
            }
            catch { return string.Empty; }
        }

        public async Task<bool> Delete(string commentId)
        {
            try
            {
                filter = Builders<Comment>.Filter.Eq(p => p.Id, commentId);
                await collection.DeleteOneAsync(filter);
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> Update(Comment comment)
        {
            filter = Builders<Comment>.Filter.Eq(p => p.Id, comment.Id);
            var update = Builders<Comment>.Update
                            .Set(s => s.Commentator, comment.Commentator)
                            .Set(s => s.CommentTime, comment.CommentTime)
                            .Set(s => s.EditedTime, comment.EditedTime)
                            .Set(s => s.Likes, comment.Likes)
                            .Set(s => s.Replies, comment.Replies)
                            .Set(s => s.FeedId, comment.FeedId)
                            .Set(s => s.FeedType, comment.FeedType)
                            .Set(s => s.Content, comment.Content);

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

        public async Task<Comment> Get(string commentId)
        {
            filter = Builders<Comment>.Filter.Eq(p => p.Id, commentId);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Comment>> GetByPost(StatusPost post, int skip, int limit)
        {
            return await collection.Find($@"{{'PostId':'{post.Id}','PostType':{(int)PostType.StatusPost}}}").Skip(skip).Limit(limit).ToListAsync();
        }

        public async Task<List<Comment>> GetByPost(JobPost post, int skip, int limit)
        {
            return await collection.Find($@"{{'PostId':'{post.Id}','PostType':{(int)PostType.JobPost}}}").Skip(skip).Limit(limit).ToListAsync();
        }
    }
}
