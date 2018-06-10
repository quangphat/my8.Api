using my8.Api.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoI = my8.Api.Interfaces.Mongo;
using NeoI = my8.Api.Interfaces.Neo4j;
using my8.Api.Models;
using AutoMapper;
namespace my8.Api.Business
{
    public class JobPostBusiness : IJobPostBusiness
    {
        MongoI.IJobPostRepository m_JobPostRepositoryM;
        public JobPostBusiness(MongoI.IJobPostRepository jobpostRepoM)
        {
            m_JobPostRepositoryM = jobpostRepoM;
        }
        public async Task<bool> Active(string postId, bool active)
        {
            return await m_JobPostRepositoryM.Active(postId, active);
        }

        public async Task<bool> DeletePost(string postId)
        {
            return await m_JobPostRepositoryM.DeletePost(postId);
        }

        public async Task<JobPost> Get(string postId)
        {
            return await m_JobPostRepositoryM.Get(postId);
        }

        public async Task<List<JobPost>> GetByAuthor(Author author)
        {
            return await m_JobPostRepositoryM.GetByAuthor(author);
        }

        public async Task<List<JobPost>> Gets(string[] id)
        {
            return await m_JobPostRepositoryM.Gets(id);
        }

        public async Task<JobPost> Post(JobPost post)
        {
            post.PostTime = DateTime.UtcNow;
            post.PostTimeUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            string id = await m_JobPostRepositoryM.Post(post);
            post.Id = id;
            return post;
        }

        public async Task<bool> UpdateComments(JobPost post)
        {
            return await m_JobPostRepositoryM.UpdateComments(post);
        }

        public async Task<bool> UpdateLikes(string postId,bool like)
        {
            JobPost post = await m_JobPostRepositoryM.Get(postId);
            if (post != null)
            {
                if (like)
                {
                    post.Likes += 1;
                }
                else
                {
                    post.Likes -= 1;
                }
                return await m_JobPostRepositoryM.UpdateLikes(post);
            }
            return false;
        }

        public async Task<bool> UpdatePost(JobPost post)
        {
            return await m_JobPostRepositoryM.UpdatePost(post);
        }

        public async Task<bool> UpdateShares(JobPost post)
        {
            return await m_JobPostRepositoryM.UpdateShares(post);
        }

        public async Task<bool> UpdateViews(JobPost post)
        {
            return await m_JobPostRepositoryM.UpdateViews(post);
        }
    }
}
