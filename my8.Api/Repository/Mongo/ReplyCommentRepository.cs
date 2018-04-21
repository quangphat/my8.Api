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
    public class ReplyCommentRepository:MongoRepositoryBase,IReplyCommentRepository
    {
		IMongoCollection<ReplyComment> collection;
		FilterDefinition<ReplyComment> filter = FilterDefinition<ReplyComment>.Empty;
        public ReplyCommentRepository(IOptions<MongoConnection> setting) : base(setting) 
        {
            collection = _db.GetCollection<ReplyComment>("ReplyComment");
        }
        public async Task<string> Create(ReplyComment replycomment)
        {
            try
            {
                await collection.InsertOneAsync(replycomment);
                return replycomment.Id;
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<ReplyComment> Get(string replyId)
        {
            filter = Builders<ReplyComment>.Filter.Eq(p => p.Id, replyId);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<bool> Update(ReplyComment replycomment)
        {
            var filter = Builders<ReplyComment>.Filter.Eq(p => p.Id, replycomment.Id);
            var update = Builders<ReplyComment>.Update
                            .Set(s => s.ReplyBy, replycomment.ReplyBy)
                            .Set(s => s.ReplyTime, replycomment.ReplyTime)
                            .Set(s => s.EditedTime, replycomment.EditedTime)
                            .Set(s => s.Likes, replycomment.Likes)
                            .Set(s => s.PostId, replycomment.PostId)
                            .Set(s => s.CommmentId, replycomment.CommmentId)
                            .Set(s => s.Content, replycomment.Content);

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
        public async Task<bool> Delete(string id)
        {
            try
            {
                filter = Builders<ReplyComment>.Filter.Eq(p => p.Id, id);
                await collection.DeleteOneAsync(filter);
                return true;
            }
            catch { return false; }
        }

        public async Task<List<ReplyComment>> GetByComment(string commentId, int skip, int limit)
        {
            return await collection.Find($@"{{'CommmentId':'{commentId}'}}").Skip(skip).Limit(limit).ToListAsync();
        }
    }
}

