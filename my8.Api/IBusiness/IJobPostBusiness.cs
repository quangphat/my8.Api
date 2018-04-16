using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface IJobPostBusiness
    {
        Task<JobPost> Post(JobPost post);
        Task<List<JobPost>> Gets(string[] id);
        Task<JobPost> Get(string postId);
        Task<List<JobPost>> GetByActor(Actor actor);
        Task<bool> UpdatePost(JobPost post);
        Task<bool> UpdateLikes(JobPost post);
        Task<bool> UpdateShares(JobPost post);
        Task<bool> UpdateComments(JobPost post);
        Task<bool> UpdateViews(JobPost post);
        Task<bool> Active(string postId, bool active);
        Task<bool> DeletePost(string postId);
    }
}
