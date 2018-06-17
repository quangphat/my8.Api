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
    public class NotificationBusiness : INotificationBusiness
    {
        MongoI.INotificationRepository m_CommentNotifyRepositoryM;
        public NotificationBusiness(MongoI.INotificationRepository commentnotifyRepoM)
        {
            m_CommentNotifyRepositoryM = commentnotifyRepoM;
        }
        public async Task<Notification> Create(Notification commentnotify)
        {
            string id = await m_CommentNotifyRepositoryM.Create(commentnotify);
            commentnotify.Id = id;
            return commentnotify;
        }

        public async Task<Notification> Get(string commentnotifyId)
        {
            return await m_CommentNotifyRepositoryM.Get(commentnotifyId);
        }
        public async Task<bool> Update(Notification commentnotify)
        {
            return await m_CommentNotifyRepositoryM.Update(commentnotify);
        }
        public async Task<bool> Delete(string id)
        {
            return await m_CommentNotifyRepositoryM.Delete(id);
        }
    }
}
