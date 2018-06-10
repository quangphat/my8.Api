using my8.Api.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoI = my8.Api.Interfaces.Mongo;
using my8.Api.Models;
namespace my8.Api.Business
{
    public class StatusPostBusiness : IStatusPostBusiness
    {
        MongoI.IStatusPostRepository m_StatuspostRepositoryM;
        public StatusPostBusiness(MongoI.IStatusPostRepository statuspostRepoM)
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

        public async Task<List<StatusPost>> GetByAuthor(Author author)
        {
            return await m_StatuspostRepositoryM.GetByAuthor(author);
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
            return await m_StatuspostRepositoryM.UpdateComments(post.Id);
        }

        public async Task<bool> UpdateLikes(string postId,bool like)
        {
            StatusPost post = await m_StatuspostRepositoryM.Get(postId);
            if(post!=null)
            {
                if(like)
                {
                    post.Likes += 1;
                }
                else
                {
                    post.Likes -= 1;
                }
                return await m_StatuspostRepositoryM.UpdateLikes(post);
            }
            return false;
        }

        public async Task<bool> UpdatePost(StatusPost post)
        {
            return await m_StatuspostRepositoryM.UpdatePost(post);
        }

        public async Task<bool> UpdateShares(StatusPost post)
        {
            return await m_StatuspostRepositoryM.UpdateShares(post);
        }

        public async Task<bool> UpdateViews(StatusPost post)
        {
            return await m_StatuspostRepositoryM.UpdateViews(post);
        }
    }
}
