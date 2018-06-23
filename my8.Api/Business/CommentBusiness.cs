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
            comment.CommentTime = DateTime.UtcNow;
            comment.CommentTimeUnix = Utils.GetUnixTime();
            string id = await _commentRepositoryM.Create(comment);
            if (!string.IsNullOrWhiteSpace(id))
            {
                comment.Id = id;
                bool updateComment = await _statusPostRepository.UpdateComments(comment.PostId);// update number of comment.
                long countOthersCommentator = await _notifyRepository.CountCommentator(comment.PostId, comment.PostType, comment.Commentator.AuthorId, (AuthorType)comment.Commentator.AuthorTypeId,NotifyType.Comment,comment.FeedAuthor.AuthorId);
                Notification notify = new Notification
                {
                    AuthorId = comment.Commentator.AuthorId,
                    AuthorType = (AuthorType)comment.Commentator.AuthorTypeId,
                    AuthorDisplayName = comment.Commentator.DisplayName,
                    NotifyType = NotifyType.Comment,
                    NotifyTimeUnix = comment.CommentTimeUnix,
                    CommentId = comment.Id,
                    FeedType = comment.PostType,
                    FeedId = comment.PostId,
                    ReceiverId = comment.FeedAuthor.AuthorId,
                    ReceiverType = (AuthorType)comment.FeedAuthor.AuthorTypeId,
                    OthersCommentator = countOthersCommentator
                };
                Notification exist = await _notifyRepository.Get(notify.FeedId, notify.FeedType,notify.AuthorId,notify.AuthorType);
                if (exist == null)
                {
                    notify.Id = await _notifyRepository.Create(notify);
                }
                else
                {
                    notify.Id = exist.Id;
                    await _notifyRepository.Update(notify);
                }
                //CommentNotify commentNotify = new CommentNotify
                //{
                //    FeedType = comment.PostType,
                //    CommentatorId = comment.Commentator.AuthorId,
                //    CommentatorType = (AuthorType)comment.Commentator.AuthorTypeId,
                //    FeedId = comment.PostId
                //};
                
                //await _commentNotifyRepository.Create(commentNotify);
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
