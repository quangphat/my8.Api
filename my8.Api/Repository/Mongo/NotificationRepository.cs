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
    public class NotificationRepository : MongoRepositoryBase,INotificationRepository
    {
		IMongoCollection<Notification> collection;
		FilterDefinition<Notification> filter = FilterDefinition<Notification>.Empty;
        public NotificationRepository(IOptions<MongoConnection> setting) : base(setting) 
        {
            collection = _db.GetCollection<Notification>("Notification");
        }
        public async Task<string> Create(Notification commentnotify)
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

        public async Task<Notification> Get(string id)
        {
            filter = Builders<Notification>.Filter.Eq(p => p.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<Notification> Get(string feedId, PostType feedType,string authorId,AuthorType authorType)
        {
            return await collection.Find($@"{{FeedId:'{feedId}',FeedType:{(int)feedType},AuthorId:'{authorId}',AuthorType:{(int)authorType}}}").FirstOrDefaultAsync();
        }
        public async Task<bool> Update(Notification notification)
        {
            var filter = Builders<Notification>.Filter.Eq(p => p.Id, notification.Id);
            var update = Builders<Notification>.Update
                            .Set(s => s.AuthorId, notification.AuthorId)
                            .Set(s => s.AuthorDisplayName, notification.AuthorDisplayName)
                            .Set(s => s.AuthorType, notification.AuthorType)
                            .Set(s => s.NotifyType, notification.NotifyType)
                            .Set(s => s.CommentId, notification.CommentId)
                            .Set(s => s.FeedId, notification.FeedId)
                            .Set(s => s.FeedType, notification.FeedType)
                            .Set(s => s.ReceiverId, notification.ReceiverId)
                            .Set(s => s.ReceiverType, notification.ReceiverType)
                            .Set(s => s.NotifyTimeUnix, notification.NotifyTimeUnix)
                            .Set(s => s.OthersCommentator, notification.OthersCommentator);

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
                filter = Builders<Notification>.Filter.Eq(p => p.Id, id);
                await collection.DeleteOneAsync(filter);
                return true;
            }
            catch { return false; }
        }
        public async Task<long> CountCommentator(string feedId, PostType postType, string exceptCommentatorId, AuthorType exceptAuthorType, NotifyType notifyType, string feedAuthorId)
        {
            return await collection.CountAsync($@"{{FeedId:'{feedId}',FeedType:{(int)postType},AuthorId:{{$nin:['{exceptCommentatorId}','{feedAuthorId}']}},NotifyType:{(int)notifyType},AuthorType:{(int)exceptAuthorType}}}");
        }

    }
}

