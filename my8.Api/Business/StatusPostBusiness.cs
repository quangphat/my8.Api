using my8.Api.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoI = my8.Api.Interfaces.Mongo;
using my8.Api.Models;
using my8.Api.my8Enum;
using my8.Api.Infrastructures;

namespace my8.Api.Business
{
    public class StatusPostBusiness :BaseBusiness, IStatusPostBusiness
    {
        MongoI.IStatusPostRepository m_StatuspostRepositoryM;
        public StatusPostBusiness(MongoI.IStatusPostRepository statuspostRepoM,CurrentProcess process) :base(process)
        {
            m_StatuspostRepositoryM = statuspostRepoM;
        }

        public async Task<bool> Active(string postId, bool active)
        {
            return await m_StatuspostRepositoryM.Active(postId, active);
        }

        public async Task<bool> DeletePost(string postId)
        {
            return await m_StatuspostRepositoryM.DeletePost(postId);
        }

        public async Task<StatusPost> Get(string postId)
        {
            return await m_StatuspostRepositoryM.Get(postId);
        }

        public async Task<List<StatusPost>> GetByAuthorPerson(string personId, int type, int page, int limit,long lastPostTimeUnix = 0)
        {
            if (type == (int)AuthorType.Person)
            {
                return await m_StatuspostRepositoryM.GetByAuthorPerson(personId, page, limit);
            }
            return null;
        }

        public async Task<List<StatusPost>> Gets(string[] id)
        {
            return await m_StatuspostRepositoryM.Gets(id);
        }

        public async Task<StatusPost> Post(StatusPost post)
        {
            post.PostTime = DateTime.UtcNow;
            post.PostTimeUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            string id = await m_StatuspostRepositoryM.Post(post);
            post.Id = id;
            return post;
        }

        public async Task<bool> UpdateComments(StatusPost post)
        {
            return await m_StatuspostRepositoryM.UpdateComments(post.Id, true);
        }

        public async Task<bool> UpdateLikes(string postId, bool like)
        {
            if (string.IsNullOrWhiteSpace(postId)) return false;
            if (like)
            {
                return await m_StatuspostRepositoryM.Like(postId);
            }
            else
            {
                return await m_StatuspostRepositoryM.UnLike(postId);
            }
        }

        public async Task<bool> UpdatePost(StatusPost post)
        {
            return await m_StatuspostRepositoryM.UpdatePost(post);
        }

        public async Task<bool> UpdateShares(StatusPost post)
        {
            return await m_StatuspostRepositoryM.UpdateShares(post.Id, true);
        }

        public async Task<bool> UpdateViews(StatusPost post)
        {
            return await m_StatuspostRepositoryM.UpdateViews(post.Id, true);
        }
    }
}
