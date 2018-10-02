using MongoDB.Bson;
using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IStatusPostRepository
    {
        Task<List<StatusPost>> Gets(string[] id);
        Task<StatusPost> Get(string postId);
        Task<List<StatusPost>> GetByAuthorPerson(string personId,int page, int limit, long lastPostTimeUnix = 0);
        Task<string> Post(StatusPost post);
        Task<bool> UpdatePost(StatusPost post);

        Task<bool> Like(string postId);
        Task<bool> UnLike(string postId);
        Task<bool> UpdateShares(string postId, bool inc);
        Task<bool> UpdateComments(string postId,bool inc);
        Task<bool> UpdateViews(string postId, bool inc);
        Task<bool> Active(string postId,bool active);
        Task<bool> DeletePost(string postId);
    }
}
