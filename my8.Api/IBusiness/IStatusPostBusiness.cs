using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface IStatusPostBusiness
    {
        Task<StatusPost> Post(StatusPost post);
        Task<List<StatusPost>> Gets(string[] id);
        Task<StatusPost> Get(string postId);
        Task<List<StatusPost>> GetByAuthor(Author author);
        Task<bool> UpdatePost(StatusPost post);
        Task<bool> UpdateLikes(string postId,bool like);
        Task<bool> UpdateShares(StatusPost post);
        Task<bool> UpdateComments(StatusPost post);
        Task<bool> UpdateViews(StatusPost post);
        Task<bool> Active(string postId, bool active);
        Task<bool> DeletePost(string postId);
    }
}
