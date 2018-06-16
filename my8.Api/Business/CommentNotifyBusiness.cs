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
    public class CommentNotifyBusiness : ICommentNotifyBusiness
    {
        MongoI.ICommentNotifyRepository m_CommentNotifyRepositoryM;
        public CommentNotifyBusiness(MongoI.ICommentNotifyRepository commentnotifyRepoM)
        {
            m_CommentNotifyRepositoryM = commentnotifyRepoM;
        }
        public async Task<CommentNotify> Create(CommentNotify commentnotify)
        {
            string id = await m_CommentNotifyRepositoryM.Create(commentnotify);
            commentnotify.Id = id;
            return commentnotify;
        }

        public async Task<CommentNotify> Get(string commentnotifyId)
        {
            return await m_CommentNotifyRepositoryM.Get(commentnotifyId);
        }
        public async Task<bool> Update(CommentNotify commentnotify)
        {
            return await m_CommentNotifyRepositoryM.Update(commentnotify);
        }
        public async Task<bool> Delete(string id)
        {
            return await m_CommentNotifyRepositoryM.Delete(id);
        }
        public async Task<List<CommentNotify>> Search(string searchStr)
        {
            searchStr = searchStr.ToLower();
            return await m_CommentNotifyRepositoryM.Search(searchStr);
        }
    }
}
