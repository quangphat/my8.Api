using my8.Api.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoI = my8.Api.Interfaces.Mongo;
using NeoI = my8.Api.Interfaces.Neo4j;
using my8.Api.Models;
using AutoMapper;
using my8.Api.my8Enum;
using my8.Api.Infrastructures;

namespace my8.Api.Business
{
    public class CommentBusiness : ICommentBusiness
    {
        MongoI.ICommentRepository _commentRepositoryM;
        MongoI.IStatusPostRepository _statusPostRepository;
        MongoI.INotificationRepository _notifyRepository;
        MongoI.ICommentNotifyRepository _commentNotifyRepository;
        public CommentBusiness(MongoI.ICommentRepository commentRepoM, MongoI.IStatusPostRepository statusPostRepository, 
            MongoI.INotificationRepository NotifyRepository, MongoI.ICommentNotifyRepository commentNotifyRepository)
        {
            _commentRepositoryM = commentRepoM;
            _statusPostRepository = statusPostRepository;
            _notifyRepository = NotifyRepository;
            _commentNotifyRepository = commentNotifyRepository;
        }
        public async Task<Notification> Create(Comment comment)
        {
            if (comment == null || comment.Feed == null) return null;
            string[] receiversId = null;
            if(comment.Feed.PostingAs== ActionAsType.Person)
            {
                receiversId = new string[] { comment.Feed.PersonId };
            }
            else if(comment.Feed.PostingAs == ActionAsType.Page)
            {
                //Get list page's admins
            }
            else if(comment.Feed.PostingAs == ActionAsType.Community)
            {
                //get list pcommunity's admin
            }
            comment.CommentTime = DateTime.UtcNow;
            comment.CommentTimeUnix = Utils.GetUnixTime();

            string id = await _commentRepositoryM.Create(comment);
            if (!string.IsNullOrWhiteSpace(id))
            {
                comment.Id = id;
                await _statusPostRepository.UpdateComments(comment.FeedId,true);// update number of comment.
                string code = Utils.GenerateNotifyCodeCount(comment);
                long countOthersCommentator = await _notifyRepository.CountOthers(code, comment.Commentator.AuthorId,comment.Feed.PersonId);
                Notification notify = new Notification
                {
                    AuthorId = comment.Commentator.AuthorId,
                    AuthorType = (AuthorType)comment.Commentator.AuthorTypeId,
                    AuthorDisplayName = comment.Commentator.DisplayName,
                    NotifyType = NotifyType.Comment,
                    NotifyTimeUnix = comment.CommentTimeUnix,
                    CommentId = comment.Id,
                    FeedType = comment.FeedType,
                    FeedId = comment.FeedId,
                    ReceiversId = receiversId,
                    TargetId = comment.Feed.PostBy.AuthorId,
                    TargetType = (NotificationTargetType)comment.Feed.PostingAs,
                    OthersCount = countOthersCommentator
                };
                string codeExist = Utils.GenerateNotifyCodeExist(notify);
                notify.CodeCount = code;
                notify.CodeExist = codeExist;
                Notification exist = await _notifyRepository.GetByCodeExist(codeExist);
                if (exist == null)
                {
                    notify.Id = await _notifyRepository.Create(notify);
                }
                else
                {
                    notify.Id = exist.Id;
                    await _notifyRepository.Update(notify);
                }
                if (string.IsNullOrWhiteSpace(notify.Id)) return null;
                return notify;
            }
            return null;
        }

        public async Task<Comment> Get(string commentId)
        {
            return await _commentRepositoryM.Get(commentId);
        }
        public async Task<bool> Update(Comment comment)
        {
            return await _commentRepositoryM.Update(comment);
        }
        public async Task<bool> Delete(string id)
        {
            return await _commentRepositoryM.Delete(id);
        }

        public async Task<List<Comment>> GetByPost(string postId, int postType, int skip)
        {
            if (postType == (int)PostType.StatusPost)
            {
                StatusPost post = new StatusPost();
                post.Id = postId;
                List<Comment> comments = await _commentRepositoryM.GetByPost(post, skip, Utils.LIMIT_ROW_COMMENT);
                return comments.OrderBy(p => p.CommentTime).ToList();
            }
            if (postType == (int)PostType.JobPost)
            {
                JobPost post = new JobPost();
                post.Id = postId;
                return await _commentRepositoryM.GetByPost(post, skip, Utils.LIMIT_ROW_COMMENT);
            }
            return null;
        }
    }
}
