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

namespace my8.Api.Business
{
    public class ReplyCommentBusiness : IReplyCommentBusiness
    {
        MongoI.IReplyCommentRepository m_ReplyCommentRepositoryM;
        public ReplyCommentBusiness(MongoI.IReplyCommentRepository replycommentRepoM)
        {
            m_ReplyCommentRepositoryM = replycommentRepoM;
        }
        public async Task<ReplyComment> Create(ReplyComment replycomment)
        {
            replycomment.ReplyTime = DateTime.UtcNow;
            replycomment.ReplyTimeUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            string id = await m_ReplyCommentRepositoryM.Create(replycomment);
            replycomment.Id = id;
            return replycomment;
        }

        public async Task<ReplyComment> Get(string replycommentId)
        {
            return await m_ReplyCommentRepositoryM.Get(replycommentId);
        }
        public async Task<bool> Update(ReplyComment replycomment)
        {
            return await m_ReplyCommentRepositoryM.Update(replycomment);
        }
        public async Task<bool> Delete(string id)
        {
            return await m_ReplyCommentRepositoryM.Delete(id);
        }

        public async Task<List<ReplyComment>> GetByComment(string commentId, int skip)
        {
            return await m_ReplyCommentRepositoryM.GetByComment(commentId, skip, Utils.LIMIT_ROW_COMMENT);
        }
    }
}
