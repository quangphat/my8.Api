using my8.Api.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoI = my8.Api.Interfaces.Mongo;
using NeoI = my8.Api.Interfaces.Neo4j;
using my8.Api.Models;
using AutoMapper;
namespace my8.Api.Business
{
    public class FeedLikeBusiness : IFeedLikeBusiness
    {
        MongoI.IFeedLikeRepository m_FeedLikeRepositoryM;
        public FeedLikeBusiness(MongoI.IFeedLikeRepository feedlikeRepoM)
        {
            m_FeedLikeRepositoryM = feedlikeRepoM;
        }

        public async Task<bool> Like(FeedLike feedlike)
        {
            FeedLike exist = await m_FeedLikeRepositoryM.Get(feedlike);
            if(exist==null)
            {
                return await m_FeedLikeRepositoryM.Create(feedlike);
            }
            else
            {
                return await m_FeedLikeRepositoryM.Update(feedlike);
            }
        }
    }
}
