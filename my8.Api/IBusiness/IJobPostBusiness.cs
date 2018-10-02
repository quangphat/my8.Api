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
        Task<List<JobPost>> GetByAuthorPerson(string personId, int type, int page, int limit, long lastPostTimeUnix = 0);
        //Task<List<JobPost>> GetByAuthor(Author author);
        Task<bool> UpdatePost(JobPost post);
        Task<bool> UpdateLikes(string postId,bool like);
        Task<bool> UpdateShares(JobPost post);
        Task<bool> UpdateComments(JobPost post);
        Task<bool> UpdateViews(JobPost post);
        Task<bool> Active(string postId, bool active);
        Task<bool> DeletePost(string postId);
    }
}
