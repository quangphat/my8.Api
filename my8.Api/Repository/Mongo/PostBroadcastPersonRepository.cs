using AutoMapper;
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
    public class PostBroadcastPersonRepository:MongoRepositoryBase<PostBroadcastPerson>,IPostBroadcastPersonRepository
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
        public async Task<bool> Like(string broadcastId,bool like)
        {
            var update = Builders<PostBroadcastPerson>.Update
                            .Set(s => s.Like, like);
            try
            {
                await collection.UpdateOneAsync($@"{{_id:ObjectId('{broadcastId}')}}",update);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<List<PostBroadcastPerson>> GetByPerson(string personId)
        {
            //filter = Builders<PostBroadcastPerson>.Filter.Regex("");
            List<PostBroadcastPerson> lstPost = await collection.Find($@"{{ReceiversId:'{personId}'}}").ToListAsync();
            return lstPost;
        }
        public async Task<List<PostBroadcastPerson>> GetByPerson(string personId,int skip,int limit)
        {
            //filter = Builders<PostBroadcastPerson>.Filter.Eq(p => p.ReceiversId, personId);
            List<PostBroadcastPerson> lstPost = await collection.Find($@"{{'ReceiverId':'{personId}'}}").Sort("{PostTime:1}").Skip(skip).Limit(limit).ToListAsync();
            return lstPost;
        }

        public async Task<bool> HidePost(PostBroadcastPerson post)
        {
            try
            {
                bool moveResult = await MovePostToHidden(post);
                if (moveResult == true)
                {
                    var filterBuilder = Builders<PostBroadcastPerson>.Filter;
                    filter = filterBuilder.Eq(p => p.PostId, post.PostId) & filterBuilder.Eq(p => p.ReceiverId, post.ReceiverId) & filterBuilder.Eq(p => p.PostType, post.PostType);
                    await collection.DeleteOneAsync(filter);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private async Task<bool> MovePostToHidden(PostBroadcastPerson post)
        {
            try
            {
                PostBroadcastPersonHidden hidden = Mapper.Map<PostBroadcastPersonHidden>(post);
                if(hidden!=null)
                {
                    IMongoCollection<PostBroadcastPersonHidden> hiddenCollection = _db.GetCollection<PostBroadcastPersonHidden>("PostBroadcastPersonHidden");
                    await hiddenCollection.InsertOneAsync(hidden);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}

