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

namespace my8.Api.Business
{
    public class PostBroadcastPersonBusiness : IPostBroadcastPersonBusiness
    {
        MongoI.IPostBroadcastPersonRepository m_PostbroadcastpersonRepositoryM;
        NeoI.IPersonRepository  m_PersonRepository;
        NeoI.IPageRepository m_PageRepository;
        NeoI.IClubRepository m_ClubRepository;
        public PostBroadcastPersonBusiness(MongoI.IPostBroadcastPersonRepository postbroadcastpersonRepoM, NeoI.IPersonRepository personRepository,NeoI.IPageRepository pageRepository,NeoI.IClubRepository clubRepository)
        {
            m_PostbroadcastpersonRepositoryM = postbroadcastpersonRepoM;
            m_PersonRepository = personRepository;
            m_PageRepository = pageRepository;
            m_ClubRepository = clubRepository;
        }
        public async Task<bool> BroadcastToPerson(StatusPost post)
        {
            bool result = await CreatePostBroadcastAsync(post);
            return result;
        }

        public async Task<bool> BroadcastToPerson(JobPost post)
        {
            bool result = await CreatePostBroadcastAsync(post);
            return result;
        }
        private async Task<List<PostBroadcastPerson>> GetByPerson(string personId)
        {
            return await m_PostbroadcastpersonRepositoryM.GetByPerson(personId);
        }
        public async Task<List<PostBroadcastPerson>> GetByPerson(string personId, int skip, int limit)
        {
            return await m_PostbroadcastpersonRepositoryM.GetByPerson(personId,skip,limit);
        }
        private async Task<List<PersonAllin>> GetPersonInvolve(int actorType,string actorId)
        {
            IEnumerable<PersonAllin> people = null;
            if (actorType == (int)ActorTypeEnum.Person)
            {
                people = await m_PersonRepository.GetFriends(actorId);
            }
            if (actorType == (int)ActorTypeEnum.Page)
            {
                people = await m_PageRepository.GetPersonFollow(actorId);
            }
            if (actorType == (int)ActorTypeEnum.Club)
            {
                people = await m_ClubRepository.GetMembers(actorId);
            }
            return people.ToList();
        }
        private async Task<bool> CreatePostBroadcastAsync(StatusPost post)
        {
            try
            {
                List<PersonAllin> people = await GetPersonInvolve(post.PostBy.ActorTypeId, post.PostBy.ActorId);
                if (people == null || people.Count==0) return false;
                for (int i = 0; i < people.Count; i++)
                {
                    await Task.Run(() =>
                    {
                        PostBroadcastPerson postBroadcast = new PostBroadcastPerson();
                        postBroadcast.PostId = post.Id;
                        postBroadcast.PersonId = people[i].Person.PersonId;
                        postBroadcast.PostType = PostTypeEnum.StatusPost;
                        m_PostbroadcastpersonRepositoryM.Create(postBroadcast);
                    });
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private async Task<bool> CreatePostBroadcastAsync(JobPost post)
        {
            try
            {
                List<PersonAllin> people = await GetPersonInvolve(post.PostBy.ActorTypeId, post.PostBy.ActorId);
                if (people == null) return false;
                for (int i = 0; i < people.Count; i++)
                {
                    await Task.Run(() =>
                    {
                        PostBroadcastPerson postBroadcast = new PostBroadcastPerson();
                        postBroadcast.PostId = post.Id;
                        postBroadcast.PersonId = people[i].Person.PersonId;
                        postBroadcast.PostType = PostTypeEnum.JobPost;
                        m_PostbroadcastpersonRepositoryM.Create(postBroadcast);
                    });
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> HidePost(PostBroadcastPerson post)
        {
            return await m_PostbroadcastpersonRepositoryM.HidePost(post);
        }

        
    }
}
