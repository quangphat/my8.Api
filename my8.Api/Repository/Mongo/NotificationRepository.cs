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
    public class NotificationRepository : MongoRepositoryBase<Notification>,INotificationRepository
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
        public async Task<Notification> GetByCodeExist(string code)
        {
            return await collection.Find($@"{{CodeExist:'{code}'}}").FirstOrDefaultAsync();
        }
        public async Task<bool> Update(Notification notification)
        {
            var filter = Builders<Notification>.Filter.Eq(p => p.Id, notification.Id);
            var update = Builders<Notification>.Update
                            .Set(s => s.AuthorId, notification.AuthorId)
                            .Set(s => s.CodeCount, notification.CodeCount)
                            .Set(s => s.CodeExist, notification.CodeExist)
                            .Set(s => s.AuthorDisplayName, notification.AuthorDisplayName)
                            .Set(s => s.AuthorType, notification.AuthorType)
                            .Set(s => s.NotifyType, notification.NotifyType)
                            .Set(s => s.CommentId, notification.CommentId)
                            .Set(s => s.FeedId, notification.FeedId)
                            .Set(s => s.FeedType, notification.FeedType)
                            .Set(s => s.ReceiversId, notification.ReceiversId)
                            .Set(s => s.TargetId, notification.TargetId)
                            .Set(s => s.TargetType, notification.TargetType)
                            .Set(s => s.NotifyTimeUnix, notification.NotifyTimeUnix)
                            .Set(s => s.OthersCount, notification.OthersCount);

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
        //public async Task<long> CountOthers(string feedId, PostType postType, string exceptCommentatorId, AuthorType exceptAuthorType, NotifyType notifyType, string exceptPersonId)
        //{
        //    //if(exceptAuthorType == AuthorType.Person)
        //        return await collection.CountAsync($@"{{FeedId:'{feedId}',FeedType:{(int)postType},AuthorId:{{$nin:['{exceptCommentatorId}','{exceptPersonId}']}},NotifyType:{(int)notifyType},AuthorType:{(int)exceptAuthorType}}}");

        //}
        public async Task<long> CountOthers(string code,string actionAuthorId, string feedAuthorId)
        {
            return await collection.CountAsync($@"{{CodeCount:'{code}',AuthorId:{{$nin:['{actionAuthorId}','{feedAuthorId}']}}}}");
        }
    }
}

