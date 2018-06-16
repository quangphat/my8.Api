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
        MongoI.IPostBroadcastPersonRepository _postBroadcastPersonRepository;
        MongoI.IStatusPostRepository _statusPostRepository;
        MongoI.IJobPostRepository _jobPostRepository;
        public FeedLikeBusiness(MongoI.IFeedLikeRepository feedlikeRepoM,
            MongoI.IPostBroadcastPersonRepository postBroadcastPersonRepository,
            MongoI.IStatusPostRepository statusPostRepository,
            MongoI.IJobPostRepository jobPostRepository)
        {
            m_FeedLikeRepositoryM = feedlikeRepoM;
            _postBroadcastPersonRepository = postBroadcastPersonRepository;
            _statusPostRepository = statusPostRepository;
            _jobPostRepository = jobPostRepository;
        }

        public async Task<bool> Like(FeedLike feedlike)
        {
            FeedLike exist = await m_FeedLikeRepositoryM.Get(feedlike);
            bool result = false;
            if (exist==null)
            {
                result = await m_FeedLikeRepositoryM.Create(feedlike);
            }
            else
            {
                result = await m_FeedLikeRepositoryM.Update(feedlike);
            }
            if(result)
            {
                result = await _postBroadcastPersonRepository.Like(feedlike.BroadCastId, feedlike.Liked);
                if(result)
                {
                    result = await UpdateLikes(feedlike);
                    return result;
                }
            }
            return result;
        }
        private async Task<bool> UpdateLikes(FeedLike feedlike)
        {
            if(feedlike.FeedType== my8Enum.PostType.StatusPost)
            {
                StatusPost post = await _statusPostRepository.Get(feedlike.FeedId);
                if (post != null)
                {
                    if (feedlike.Liked)
                    {
                        post.Likes += 1;
                    }
                    else
                    {
                        post.Likes -= 1;
                    }
                    return await _statusPostRepository.UpdateLikes(post);
                }
            }
            else if(feedlike.FeedType== my8Enum.PostType.JobPost)
            {
                JobPost post = await _jobPostRepository.Get(feedlike.FeedId);
                if (post != null)
                {
                    if (feedlike.Liked)
                    {
                        post.Likes += 1;
                    }
                    else
                    {
                        post.Likes -= 1;
                    }
                    return await _jobPostRepository.UpdateLikes(post);
                }
                return false;
            }
            return false;
        }
    }
}
