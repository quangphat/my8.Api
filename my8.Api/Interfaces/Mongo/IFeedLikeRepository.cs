using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IFeedLikeRepository
    {
        Task<FeedLike> Get(FeedLike feedlike);
        Task<bool> Create(FeedLike feedlike);
        Task<bool> Update(FeedLike feedlike);
    }
}

