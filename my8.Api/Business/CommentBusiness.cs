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
        public CommentBusiness(MongoI.ICommentRepository commentRepoM)
        {
            m_CommentRepositoryM = commentRepoM;
        }
        public async Task<Comment> Create(Comment comment)
        {
            string id = await m_CommentRepositoryM.Create(comment);
            comment.Id = id;
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
                return await m_CommentRepositoryM.GetByPost(post,skip,Utils.LIMIT_ROW);
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
