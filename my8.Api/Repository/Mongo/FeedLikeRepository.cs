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
    public class FeedLikeRepository:MongoRepositoryBase,IFeedLikeRepository
    {
		IMongoCollection<FeedLike> collection;
		FilterDefinition<FeedLike> filter = FilterDefinition<FeedLike>.Empty;
        public FeedLikeRepository(IOptions<MongoConnection> setting) : base(setting) 
        {
            collection = _db.GetCollection<FeedLike>("FeedLike");
        }

        public async Task<bool> Update(FeedLike feedlike)
        {
            var update = Builders<FeedLike>.Update
                            .Set(s => s.LikedTimeUnix, feedlike.LikedTimeUnix)
                            .Set(s => s.Liked, feedlike.Liked);

            try
            {
                await collection.UpdateOneAsync($@"{{FeedId:'{feedlike.FeedId}',FeedType:{(int)feedlike.FeedType},'Author.AuthorId':'{feedlike.Author.AuthorId}'}}", update);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<FeedLike> Get(FeedLike feedlike)
        {
            try
            {
                FeedLike obj = await collection.Find($@"{{FeedId:'{feedlike.FeedId}',FeedType:{(int)feedlike.FeedType},'Author.AuthorId':'{feedlike.Author.AuthorId}'}}").FirstOrDefaultAsync();
                return obj;
            }
            catch(Exception e)
            {
                return null;
            }

        }

        public async Task<string> Create(FeedLike feedlike)
        {
            try
            {
                await collection.InsertOneAsync(feedlike);
                return feedlike.Id;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
    }
}

