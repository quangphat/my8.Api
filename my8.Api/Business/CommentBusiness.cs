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
        MongoI.ICommentNotifyRepository _commentNotify;
        public CommentBusiness(MongoI.ICommentRepository commentRepoM,MongoI.IStatusPostRepository statusPostRepository,MongoI.ICommentNotifyRepository commentNotifyRepository)
        {
            _commentRepositoryM = commentRepoM;
            _statusPostRepository = statusPostRepository;
            _commentNotify = commentNotifyRepository;
        }
        public async Task<CommentNotify> Create(Comment comment)
        {
            comment.CommentTime = DateTime.UtcNow;
            comment.CommentTimeUnix = Utils.GetUnixTime();
            string id = await _commentRepositoryM.Create(comment);
            if(!string.IsNullOrWhiteSpace(id))
            {
                comment.Id = id;
                bool updateComment = await _statusPostRepository.UpdateComments(comment.PostId);// update number of comment.
                CommentNotify commentNotify = new CommentNotify
                {
                    Commentator = comment.Commentator,
                    CommentTimeUnix = comment.CommentTimeUnix,
                    CommentId = comment.Id,
                    FeedType = comment.PostType,
                    FeedId = comment.PostId,
                    FeedAuthorId = comment.FeedAuthor.AuthorId,
                    FeedAuthorType = (AuthorType)comment.FeedAuthor.AuthorTypeId
            };
                commentNotify.Id = await _commentNotify.Create(commentNotify);
                
                if (string.IsNullOrWhiteSpace(commentNotify.Id)) return null;
                return commentNotify;
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

        public async Task<List<Comment>> GetByPost(string postId, int postType,int skip)
        {
            if(postType == (int)PostType.StatusPost)
            {
                StatusPost post = new StatusPost();
                post.Id = postId;
                List<Comment> comments = await _commentRepositoryM.GetByPost(post, skip, Utils.LIMIT_ROW_COMMENT);
                return comments.OrderBy(p => p.CommentTime).ToList();
            }
            if(postType == (int)PostType.JobPost)
            {
                JobPost post = new JobPost();
                post.Id = postId;
                return await _commentRepositoryM.GetByPost(post, skip, Utils.LIMIT_ROW_COMMENT);
            }
            return null;
        }
    }
}
