using MongoDB.Bson;
using my8.Api.Models.Mongo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IStatusPostRepository
    {
        Task<List<StatusPost>> Gets(string[] id);
        Task<StatusPost> GetByPostId(string postId);
        Task<List<StatusPost>> GetByAuthor(Actor actor);
        Task<bool> Post(StatusPost post);
        Task<bool> UpdatePost(StatusPost post);
        Task<bool> UpdateLikes(StatusPost post);
        Task<bool> UpdateShares(StatusPost post);
        Task<bool> UpdateComment(StatusPost post);
        Task<bool> UpdateViews(StatusPost post);
        Task<bool> Active(string postId,bool active);
        Task<bool> DeletePost(string postId);
    }
}
