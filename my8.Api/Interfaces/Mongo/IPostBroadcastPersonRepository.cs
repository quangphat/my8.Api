using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IPostBroadcastPersonRepository
    {
        Task<bool> Create(PostBroadcastPerson postbroadcastperson);
        Task<List<PostBroadcastPerson>> GetByPerson(string personId);
        Task<List<PostBroadcastPerson>> GetByPerson(string personId,int skip,int limit);
        Task<bool> HidePost(PostBroadcastPerson post);
        Task<bool> Like(string broadcastId, bool like);
    }
}

