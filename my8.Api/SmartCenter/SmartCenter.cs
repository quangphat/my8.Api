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

namespace my8.Api.SmartCenter
{
    public class SmartCenter:ISmartCenter
    {
        private const int MAX_LIMIT = 100;

        MongoI.IPostBroadcastPersonRepository m_PostbroadcastPersonRepositoryM;
        NeoI.IPersonRepository m_PersonRepository;
        NeoI.IPageRepository m_PageRepository;
        NeoI.IClubRepository m_ClubRepository;
        public SmartCenter(MongoI.IPostBroadcastPersonRepository postBroadcastPersonRepository
            ,NeoI.IPersonRepository personRepository
            ,NeoI.IPageRepository pageRepository
            ,NeoI.IClubRepository clubRepository)
        {
            m_PostbroadcastPersonRepositoryM = postBroadcastPersonRepository;
            m_PersonRepository = personRepository;
            m_PageRepository = pageRepository;
            m_ClubRepository = clubRepository;
        }
        private async Task<bool> CreatePostBroadcastAsync(StatusPost post)
        {
            try
            {
                List<PersonAllin> people = await GetPersonInvolve(post.PostBy);
                if (people == null || people.Count == 0) return false;
                List<Task> tasks = new List<Task>();
                for (int i = 0; i < people.Count - 1; i++)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        PostBroadcastPerson postBroadcast = new PostBroadcastPerson();
                        postBroadcast.PostId = post.Id;
                        postBroadcast.PersonId = people[i].Person.PersonId;
                        postBroadcast.PostType = PostTypeEnum.StatusPost;
                        post.PostTime = post.PostTime;
                        m_PostbroadcastPersonRepositoryM.Create(postBroadcast);
                    }));
                }
                await Task.WhenAll(tasks);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private async Task<bool> CreatePostBroadcastAsync(JobPost jobPost)
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
        }
        private async Task<List<PersonAllin>> GetPersonInvolve(Actor actor)
        {
            int actorType = actor.ActorTypeId;
            IEnumerable<PersonAllin> people = null;
            if (actorType == (int)ActorTypeEnum.Person)
            {
                people = await m_PersonRepository.GetFriends(actor.ActorId);
                return people.ToList();
            }
            if (actorType == (int)ActorTypeEnum.Page)
            {
                people = await m_PageRepository.GetPersonFollow(actor.ActorId);
                return people.ToList();
            }
            if (actorType == (int)ActorTypeEnum.Club)
            {
                people = await m_ClubRepository.GetMembers(actor.ActorId);
                return people.ToList();
            }
            return null;
        }
        private async Task<string[]> GetPersonIndustry(List<Industry> industries)
        {

            return null;
        }
        private async Task<string[]> GetPersonSkill(List<Skill> skills)
        {

            return null;
        }
        private async Task<string[]> GetPersonLocation(List<Location> locations)
        {

            return null;
        }
        private async Task<string[]> GetPersonDegree(List<Degree> degrees)
        {

            return null;
        }
        private async Task<string[]> GetPersonSeniority(List<SeniorityLevel> seniorityLevels)
        {

            return null;
        }
        private async Task<string[]> GetPersonEmployeeType(List<EmploymentType> employmentTypes)
        {

            return null;
        }
        public async Task<List<PostAllType>> Gets(string personId,int skip)
        {
            return null;
        }
        private async Task GetJobPostIdArray(List<PostBroadcastPerson> lstJobPostBroadCast)
        {

        }
        private async Task GetStatusPostIdArray(List<PostBroadcastPerson> lstStatusPostBroadCast)
        {

        }
    }
}
