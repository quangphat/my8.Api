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
    public class JobPostRepository:MongoRepositoryBase<JobPost>,IJobPostRepository
    {
		IMongoCollection<JobPost> collection;
		FilterDefinition<JobPost> filter = FilterDefinition<JobPost>.Empty;
        public JobPostRepository(IOptions<MongoConnection> setting) : base(setting) 
        {
            collection = _db.GetCollection<JobPost>("JobPost");
        }
        public async Task<string> Post(JobPost post)
        {
            try
            {
                await collection.InsertOneAsync(post);
                return post.Id;
            }
            catch (Exception e)
            { return string.Empty; }
        }
        public async Task<JobPost> Get(string id)
        {
            var filter = Builders<JobPost>.Filter.Eq(p => p.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<List<JobPost>> Gets(string[] id)
        {
            string temp = Utils.ArrStrIdToMongoDbId(id);
            List<JobPost> posts = await collection.Find($@"{{ _id:{{$in:[{temp}]}}}}").ToListAsync();
            return posts;
        }

        public async Task<bool> UpdatePost(JobPost post)
        {
            var filter = Builders<JobPost>.Filter.Eq(p => p.Id, post.Id);
            var update = Builders<JobPost>.Update
                            .Set(s => s.Content, post.Content)
                            .Set(s => s.EditedTime, post.EditedTime)
                            .Set(s => s.Likes, post.Likes)
                            .Set(s => s.Comments, post.Comments)
                            .Set(s => s.Shares, post.Shares)
                            .Set(s => s.Views, post.Views)
                            .Set(s => s.Images, post.Images)
                            .Set(s => s.CompanyName, post.CompanyName)
                            .Set(s => s.PersonTags, post.PersonTags)
                            .Set(s => s.IsFindJob, post.IsFindJob)
                            .Set(s => s.IsShareExperience, post.IsShareExperience)
                            .Set(s => s.IsAds, post.IsAds)
                            .Set(s => s.JobFunctionTags, post.JobFunctionTags)
                            .Set(s => s.SkillTags, post.SkillTags)
                            .Set(s => s.Locations, post.Locations)
                            .Set(s => s.MinExperience, post.MinExperience)
                            .Set(s => s.MaxExperience, post.MaxExperience)
                            .Set(s => s.Degrees, post.Degrees)
                            .Set(s => s.Active, post.Active)
                            .Set(s => s.Privacy, post.Privacy)
                            .Set(s => s.Seniority, post.Seniority)
                            .Set(s => s.EmploymentType, post.EmploymentType)
                            .Set(s => s.Applies, post.Applies)
                            .Set(s => s.MinSalary, post.MinSalary)
                            .Set(s => s.MaxSalary, post.MaxSalary)
                            .Set(s => s.Title, post.Title)
                            .Set(s => s.PersonId, post.PersonId)
                            .Set(s => s.PostingAsType, post.PostingAsType)
                            .Set(s => s.EmailToReceiveApply, post.EmailToReceiveApply);

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
        public async Task<bool> UpdateComments(string postId, bool inc)
        {
            try
            {
                if (inc)
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
        public async Task<List<JobPost>> GetByAuthorPerson(string personId, int page, int limit, long lastPostTimeUnix = 0)
        {
            return await collection.Find($@"{{'PersonId':'{personId}',PostTimeUnix:{{$gt:{lastPostTimeUnix}}}}}").Sort("{PostTimeUnix:-1}").Skip(page * limit).Limit(limit).ToListAsync();
        }

        //public async Task<List<JobPost>> GetByAuthor(Author author)
        //{
        //    var filterBuilder = Builders<JobPost>.Filter;
        //    filter = filterBuilder.Eq(p => p.PostBy.AuthorId, author.AuthorId) & filterBuilder.Eq(p => p.PostBy.AuthorTypeId, author.AuthorTypeId);
        //    List<JobPost> statusPosts = await collection.Find(filter).ToListAsync();
        //    return statusPosts;
        //}
        //public async Task<List<JobPost>> GetByAuthor(Author author, int skip, int limit, long unixPostTime = 0)
        //{
        //    var filterBuilder = Builders<StatusPost>.Filter;
        //    //filter = filterBuilder.Eq(p => p.PostBy.AuthorId, author.AuthorId) & filterBuilder.Eq(p => p.PostBy.AuthorTypeId, author.AuthorTypeId);
        //    List<JobPost> jobPosts = await collection.Find($@"{{'PostBy.AuthorId':'{author.AuthorId}','PostBy.AuthorTypeId':{author.AuthorTypeId},PostTimeUnix:{{$gt:{unixPostTime}}}}}")
        //        .Sort("{PostTimeUnix:-1}").Limit(limit).ToListAsync();
        //    return jobPosts;
        //}
        public async Task<bool> Active(string postId, bool active)
        {
            var filter = Builders<JobPost>.Filter.Eq(p => p.Id, postId);
            var update = Builders<JobPost>.Update
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
            var filter = Builders<JobPost>.Filter.Eq(p => p.Id, postId);
            try
            {
                await collection.DeleteOneAsync(filter);
                return true;
            }
            catch { return false; }
        }
    }
}

