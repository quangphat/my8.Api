using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface IFeedLikeBusiness
    {
        Task<Notification> Like(FeedLike feedlike, Feed feed);
    }
}
