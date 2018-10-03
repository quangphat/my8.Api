using Microsoft.Extensions.Options;
using MongoDB.Bson;
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
    public class CommentNotifyRepository:MongoRepositoryBase<CommentNotify>,ICommentNotifyRepository
    {
		IMongoCollection<CommentNotify> collection;
		FilterDefinition<CommentNotify> filter = FilterDefinition<CommentNotify>.Empty;
        public CommentNotifyRepository(IOptions<MongoConnection> setting) : base(setting) 
        {
            collection = _db.GetCollection<CommentNotify>("CommentNotify");
        }
        public async Task<string> Create(CommentNotify commentnotify)
        {
            try
            {
                await collection.InsertOneAsync(commentnotify);
                return commentnotify.Id;
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<CommentNotify> Get(string id)
        {
            filter = Builders<CommentNotify>.Filter.Eq(p => p.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<bool> Update(CommentNotify commentnotify)
        {
            var filter = Builders<CommentNotify>.Filter.Eq(p => p.Id, commentnotify.Id);
            var update = Builders<CommentNotify>.Update
                            .Set(s => s.Id, commentnotify.Id);
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
                filter = Builders<CommentNotify>.Filter.Eq(p => p.Id, id);
                await collection.DeleteOneAsync(filter);
                return true;
            }
            catch { return false; }
        }

        public async Task<long> CountCommentator(string feedId, PostType postType, string exceptCommentatorId, AuthorType exceptAuthorType)
        {
            return await collection.CountAsync($@"{{FeedId:'{feedId}',FeedType:{(int)postType},CommentatorId:{{$nin:['{exceptCommentatorId}']}},CommentatorType:{{$eq:{(int)exceptAuthorType}}}}}");
        }
    }
}

