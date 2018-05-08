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
        Task<List<StatusPost>> GetByAuthor(ShortPerson author);
        Task<string> Post(StatusPost post);
        Task<bool> UpdatePost(StatusPost post);
        Task<bool> UpdateLikes(StatusPost post);
        Task<bool> UpdateShares(StatusPost post);
        Task<bool> UpdateComments(StatusPost post);
        Task<bool> UpdateViews(StatusPost post);
        Task<bool> Active(string postId,bool active);
        Task<bool> DeletePost(string postId);
    }
}
