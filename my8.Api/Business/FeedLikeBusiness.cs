using my8.Api.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoI = my8.Api.Interfaces.Mongo;
using NeoI = my8.Api.Interfaces.Neo4j;
using my8.Api.Models;
using AutoMapper;
using my8.Api.Infrastructures;
using my8.Api.my8Enum;

namespace my8.Api.Business
{
    public class FeedLikeBusiness : IFeedLikeBusiness
    {
        MongoI.IFeedLikeRepository m_FeedLikeRepositoryM;
        MongoI.IPostBroadcastPersonRepository _postBroadcastPersonRepository;
        MongoI.IStatusPostRepository _statusPostRepository;
        MongoI.IJobPostRepository _jobPostRepository;
        MongoI.INotificationRepository _notifyRepository;
        public FeedLikeBusiness(MongoI.IFeedLikeRepository feedlikeRepoM,
            MongoI.IPostBroadcastPersonRepository postBroadcastPersonRepository,
            MongoI.IStatusPostRepository statusPostRepository,
            MongoI.IJobPostRepository jobPostRepository, MongoI.INotificationRepository NotifyRepository)
        {
            m_FeedLikeRepositoryM = feedlikeRepoM;
            _postBroadcastPersonRepository = postBroadcastPersonRepository;
            _statusPostRepository = statusPostRepository;
            _jobPostRepository = jobPostRepository;
            _notifyRepository = NotifyRepository;
        }

        public async Task<Notification> Like(FeedLike feedlike, Feed feed)
        {
            if (feedlike == null || feed == null) return null;
            feedlike.LikedTimeUnix = Utils.GetUnixTime();
            string[] receiversId = null;
            if (feed.PostingAs == ActionAsType.Person)
            {
                receiversId = new string[] { feed.PersonId };
            }
            else if (feed.PostingAs == ActionAsType.Page)
            {
                //Get list page's admins
            }
            else if (feed.PostingAs == ActionAsType.Community)
            {
                //get list pcommunity's admin
            }
            FeedLike exist = await m_FeedLikeRepositoryM.Get(feedlike);
            if (exist == null)
            {
                 await m_FeedLikeRepositoryM.Create(feedlike);
            }
            else
            {
                feedlike.Id = exist.Id;
                await m_FeedLikeRepositoryM.Update(feedlike);
            }
            if (!string.IsNullOrWhiteSpace(feedlike.Id))
            {
                await _postBroadcastPersonRepository.Like(feedlike.BroadCastId, feedlike.Liked);
                long countOthersCommentator = await _notifyRepository.CountOthers(feedlike.FeedId, feedlike.FeedType, feedlike.Author.AuthorId, (AuthorType)feedlike.Author.AuthorTypeId, NotifyType.Like, feedlike.FeedAuthor.AuthorId);
                Notification notify = new Notification
                {
                    AuthorId = feedlike.Author.AuthorId,
                    AuthorType = (AuthorType)feedlike.Author.AuthorTypeId,
                    AuthorDisplayName = feedlike.Author.DisplayName,
                    NotifyType = NotifyType.Like,
                    NotifyTimeUnix = feedlike.LikedTimeUnix,
                    CommentId = null,
                    FeedType = feedlike.FeedType,
                    FeedId = feedlike.FeedId,
                    ReceiversId = receiversId,
                    TargetId = feed.PostBy.AuthorId,
                    TargetType = (NotificationTargetType)feed.PostingAs,
                    OthersCount = countOthersCommentator
                };
                Notification existNotifi = await _notifyRepository.Get(notify.FeedId, notify.FeedType, notify.AuthorId, notify.AuthorType);
                if (existNotifi == null)
                {
                    notify.Id = await _notifyRepository.Create(notify);
                }
                else
                {
                    notify.Id = existNotifi.Id;
                    await _notifyRepository.Update(notify);
                }
                await UpdateLikes(feedlike);
                return notify;
            }
            return null;
        }
        private async Task<bool> UpdateLikes(FeedLike feedlike)
        {
            if (feedlike.FeedType == my8Enum.PostType.StatusPost)
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
            else if (feedlike.FeedType == my8Enum.PostType.JobPost)
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
