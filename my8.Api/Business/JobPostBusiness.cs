using my8.Api.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoI = my8.Api.Interfaces.Mongo;
using NeoI = my8.Api.Interfaces.Neo4j;
using my8.Api.Models;
using AutoMapper;
using my8.Api.my8Enum;

namespace my8.Api.Business
{
    public class JobPostBusiness : IJobPostBusiness
    {
        MongoI.IJobPostRepository _jobPostRepository;
        public JobPostBusiness(MongoI.IJobPostRepository jobpostRepoM)
        {
            _jobPostRepository = jobpostRepoM;
        }
        public async Task<bool> Active(string postId, bool active)
        {
            return await _jobPostRepository.Active(postId, active);
        }

        public async Task<bool> DeletePost(string postId)
        {
            return await _jobPostRepository.DeletePost(postId);
        }

        public async Task<JobPost> Get(string postId)
        {
            return await _jobPostRepository.Get(postId);
        }

        public async Task<List<JobPost>> GetByAuthor(Author author)
        {
            return await _jobPostRepository.GetByAuthor(author);
        }

        public async Task<List<JobPost>> Gets(string[] id)
        {
            return await _jobPostRepository.Gets(id);
        }

        public async Task<JobPost> Post(JobPost post)
        {
            post.PostTime = DateTime.UtcNow;
            post.PostTimeUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            post.Active = true;
            post.Applies = 0;
            post.Comments = 0;
            post.Likes = 0;
            post.IsAds = false;
            post.Shares = 0;
            post.Views = 0;
            post.IsFindJob = false;
            post.MinExperience = 0;
            post.MaxExperience = 0;
            post.MinSalary = 0;
            post.MaxSalary = 0;
            post.EmailToReceiveApply = string.Empty;
            post.Privacy = (int)PostPrivacyType.All;
            string id = await _jobPostRepository.Post(post);
            post.Id = id;
            return post;
        }

        public async Task<bool> UpdateComments(JobPost post)
        {
            return await _jobPostRepository.UpdateComments(post.Id,true);
        }

        public async Task<bool> UpdateLikes(string postId,bool like)
        {
            if (string.IsNullOrWhiteSpace(postId)) return false;
            if (like)
            {
                return await _jobPostRepository.Like(postId);
            }
            else
            {
                return await _jobPostRepository.UnLike(postId);
            }
        }

        public async Task<bool> UpdatePost(JobPost post)
        {
            return await _jobPostRepository.UpdatePost(post);
        }

        public async Task<bool> UpdateShares(JobPost post)
        {
            return await _jobPostRepository.UpdateShares(post.Id,true);
        }

        public async Task<bool> UpdateViews(JobPost post)
        {
            return await _jobPostRepository.UpdateViews(post.Id,true);
        }
    }
}
