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
        MongoI.ICommentRepository m_CommentRepositoryM;
        MongoI.IStatusPostRepository m_StatusPostRepository;
        public CommentBusiness(MongoI.ICommentRepository commentRepoM,MongoI.IStatusPostRepository statusPostRepository)
        {
            m_CommentRepositoryM = commentRepoM;
            m_StatusPostRepository = statusPostRepository;
        }
        public async Task<Comment> Create(Comment comment)
        {
            comment.CommentTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string id = await m_CommentRepositoryM.Create(comment);
            if(!string.IsNullOrWhiteSpace(id))
            {
                comment.Id = id;
                bool updateComment = await m_StatusPostRepository.UpdateComments(comment.PostId);
            }
            return comment;
        }

        public async Task<Comment> Get(string commentId)
        {
            return await m_CommentRepositoryM.Get(commentId);
        }
        public async Task<bool> Update(Comment comment)
        {
            return await m_CommentRepositoryM.Update(comment);
        }
        public async Task<bool> Delete(string id)
        {
            return await m_CommentRepositoryM.Delete(id);
        }

        public async Task<List<Comment>> GetByPost(string postId, int postType,int skip)
        {
            if(postType == (int)PostTypeEnum.StatusPost)
            {
                StatusPost post = new StatusPost();
                post.Id = postId;
                List<Comment> comments = await m_CommentRepositoryM.GetByPost(post, skip, Utils.LIMIT_ROW);
                return comments.OrderBy(p => p.CommentTime).ToList();
            }
            if(postType == (int)PostTypeEnum.JobPost)
            {
                JobPost post = new JobPost();
                post.Id = postId;
                return await m_CommentRepositoryM.GetByPost(post, skip, Utils.LIMIT_ROW);
            }
            return null;
        }
    }
}
