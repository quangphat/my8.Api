using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IJobPostRepository
    {
        Task<List<JobPost>> Gets(string[] id);
        Task<JobPost> Get(string postId);
        Task<List<JobPost>> GetByAuthor(Author author);
        Task<List<JobPost>> GetByAuthor(Author author, int skip, int limit, long unixPostTime);
        Task<string> Post(JobPost post);
        Task<bool> UpdatePost(JobPost post);
        Task<bool> Like(string postId);
        Task<bool> UnLike(string postId);
        Task<bool> UpdateShares(string postId, bool inc);
        Task<bool> UpdateComments(string postId, bool inc);
        Task<bool> UpdateViews(string postId, bool inc);
        Task<bool> Active(string postId, bool active);
        Task<bool> DeletePost(string postId);
    }
}

