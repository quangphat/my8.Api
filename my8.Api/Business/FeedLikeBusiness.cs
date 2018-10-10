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
    public class FeedLikeBusiness : BaseBusiness,IFeedLikeBusiness
    {
        MongoI.IFeedLikeRepository _feedLikeRepositoryM;
        MongoI.IPostBroadcastPersonRepository _postBroadcastPersonRepository;
        MongoI.IStatusPostRepository _statusPostRepository;
        MongoI.IJobPostRepository _jobPostRepository;
        MongoI.INotificationRepository _notifyRepository;
        public FeedLikeBusiness(MongoI.IFeedLikeRepository feedlikeRepoM,
            MongoI.IPostBroadcastPersonRepository postBroadcastPersonRepository,
            MongoI.IStatusPostRepository statusPostRepository,
            MongoI.IJobPostRepository jobPostRepository, MongoI.INotificationRepository NotifyRepository,CurrentProcess process):base(process)
        {
            _feedLikeRepositoryM = feedlikeRepoM;
            _postBroadcastPersonRepository = postBroadcastPersonRepository;
            _statusPostRepository = statusPostRepository;
            _jobPostRepository = jobPostRepository;
            _notifyRepository = NotifyRepository;
        }

        public async Task<Notification> Like(FeedLike feedlike)
        {
            if (CheckIsNotLogin()) return null;
            if (feedlike == null || feedlike.Feed == null) return null;
            feedlike.LikedTimeUnix = Utils.GetUnixTime();
            string[] receiversId = null;
            if (feedlike.Feed.PostingAs == ActionAsType.Person)
            {
                receiversId = new string[] { feedlike.Feed.PersonId };
            }
            else if (feedlike.Feed.PostingAs == ActionAsType.Page)
            {
                //Get list page's admin
            }
            else if (feedlike.Feed.PostingAs == ActionAsType.Community)
            {
                //get list community's admin
            }
            FeedLike exist = await _feedLikeRepositoryM.Get(feedlike);
            if (exist == null)
            {
                 await _feedLikeRepositoryM.Create(feedlike);
            }
            else
            {
                feedlike.Id = exist.Id;
                await _feedLikeRepositoryM.Update(feedlike);
            }
            if (!string.IsNullOrWhiteSpace(feedlike.Id))
            {
                await _postBroadcastPersonRepository.Like(feedlike.BroadCastId, feedlike.Liked);
                string code = Utils.GenerateNotifyCodeCount(feedlike);
                long countOthersCommentator = await _notifyRepository.CountOthers(code, feedlike.Author.AuthorId, feedlike.PersonId);
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
                    TargetId = feedlike.Feed.PostBy.AuthorId,
                    TargetType = (NotificationTargetType)feedlike.Feed.PostingAs,
                    OthersCount = countOthersCommentator
                };
                string codeExist = Utils.GenerateNotifyCodeExist(notify);
                Notification existNotifi = await _notifyRepository.GetByCodeExist(codeExist);
                notify.CodeCount = code;
                notify.CodeExist = codeExist;
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
                if (string.IsNullOrWhiteSpace(feedlike.FeedId)) return false;
                if (feedlike.Liked)
                {
                    return await _statusPostRepository.Like(feedlike.FeedId);
                }
                else
                {
                    return await _statusPostRepository.UnLike(feedlike.FeedId);
                }
            }
            else if (feedlike.FeedType == my8Enum.PostType.JobPost)
            {
                if (string.IsNullOrWhiteSpace(feedlike.FeedId)) return false;
                if (feedlike.Liked)
                {
                    return await _jobPostRepository.Like(feedlike.FeedId);
                }
                else
                {
                    return await _jobPostRepository.UnLike(feedlike.FeedId);
                }
            }
            return false;
        }
    }
}
