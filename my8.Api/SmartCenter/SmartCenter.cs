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

namespace my8.Api.SmartCenter
{
    public class SmartCenter:ISmartCenter
    {
        private const int MAX_LIMIT = 100;

        MongoI.IPostBroadcastPersonRepository m_PostbroadcastPersonRepositoryM;
        MongoI.IPersonRepository m_PersonRepositoryM;
        NeoI.IPersonRepository m_PersonRepositoryN;
        NeoI.IPageRepository m_PageRepositoryN;
        NeoI.IClubRepository m_ClubRepositoryN;
        MongoI.IStatusPostRepository m_StatusPostRepository;
        MongoI.IJobPostRepository m_JobPostRepository;
        public SmartCenter(MongoI.IPostBroadcastPersonRepository postBroadcastPersonRepository
            ,NeoI.IPersonRepository personRepositoryN
            ,NeoI.IPageRepository pageRepositoryN
            ,NeoI.IClubRepository clubRepositoryN
            ,MongoI.IPersonRepository personRepositoryM
            ,MongoI.IStatusPostRepository statusPostRepository
            ,MongoI.IJobPostRepository jobPostRepository)
        {
            m_PostbroadcastPersonRepositoryM = postBroadcastPersonRepository;
            m_PersonRepositoryN = personRepositoryN;
            m_PageRepositoryN = pageRepositoryN;
            m_ClubRepositoryN = clubRepositoryN;
            m_PersonRepositoryM = personRepositoryM;
            m_StatusPostRepository = statusPostRepository;
            m_JobPostRepository = jobPostRepository;
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
        private async Task<bool> CreatePostBroadcastAsync(StatusPost post)
        {
            try
            {
                List<PersonAllin> people = await GetPersonInvolve(post.PostBy);
                if (people == null || people.Count == 0) return false;
                List<Task> tasks = new List<Task>();
                for (int i = 0; i < people.Count; i++)
                {
                    PostBroadcastPerson postBroadcast = new PostBroadcastPerson();
                    postBroadcast.PostId = post.Id;
                    postBroadcast.PersonId = people[i].Person.PersonId;
                    postBroadcast.PostType = PostTypeEnum.StatusPost;
                    postBroadcast.KeyTime = post.PostTime;
                    tasks.Add(Task.Run(() =>
                    {
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
            string[] allPersonId = null;
            List<Task> tasks = new List<Task>();
            Task<HashSet<string>> lstPersonByActor = GetPersonIdInvolve(jobPost.PostBy);
            tasks.Add(lstPersonByActor);
            //must satisfy 
            Task<HashSet<string>> lstPersonByIndustry = GetPersonIndustry(jobPost.IndustryTags);
            tasks.Add(lstPersonByIndustry);
            //must satisfy 
            Task<HashSet<string>> lstPersonBySkill = GetPersonSkill(jobPost.SkillTags);
            tasks.Add(lstPersonBySkill);
            //optional
            //Task<HashSet<string>> lstPersonByLocation = GetPersonLocation(jobPost.Locations);
            //tasks.Add(lstPersonByLocation);
            //optional
            //Task<HashSet<string>> lstPersonByDegree = GetPersonDegree(jobPost.Degrees);
            //tasks.Add(lstPersonByDegree);
            //must satisfy 
            Task<HashSet<string>> lstPersonByExperience = GetPersonExperience(jobPost.MinExperience, jobPost.MaxExperience);
            tasks.Add(lstPersonByExperience);
            await Task.WhenAll(tasks);
            List<HashSet<string>> hashSetsMustSatisfy = new List<HashSet<string>>();
            await Task.Run(()=> {
                //must satisfy 
                hashSetsMustSatisfy.Add(lstPersonByIndustry.Result);
                hashSetsMustSatisfy.Add(lstPersonBySkill.Result);
                hashSetsMustSatisfy.Add(lstPersonByExperience.Result);
                string[] allPersonSatisfyJob = Utils.IntersectOrUnion(hashSetsMustSatisfy);

                string[] allPersonByPost = lstPersonByActor.Result.ToArray();

                //hashSetsMustSatisfy.Add(lstPersonByLocation.Result);
                //hashSetsMustSatisfy.Add(lstPersonByDegree.Result);

                allPersonId = allPersonSatisfyJob.Union(allPersonByPost).ToArray();
            });
            
            try
            {
                List<Task> lastTasks = new List<Task>();
                for (int i=0;i<allPersonId.Length;i++)
                {
                    PostBroadcastPerson postBroadcast = new PostBroadcastPerson();
                    postBroadcast.PostId = jobPost.Id;
                    postBroadcast.PersonId = allPersonId[i];
                    postBroadcast.PostType = PostTypeEnum.JobPost;
                    postBroadcast.KeyTime = jobPost.PostTime;
                    lastTasks.Add(Task.Run(() =>
                    {
                        m_PostbroadcastPersonRepositoryM.Create(postBroadcast);
                    }));
                }
                await Task.WhenAll(lastTasks);
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
                people = await m_PersonRepositoryN.GetFriends(actor.ActorId);
                return people.ToList();
            }
            if (actorType == (int)ActorTypeEnum.Page)
            {
                people = await m_PageRepositoryN.GetPersonFollow(actor.ActorId);
                return people.ToList();
            }
            if (actorType == (int)ActorTypeEnum.Club)
            {
                people = await m_ClubRepositoryN.GetMembers(actor.ActorId);
                return people.ToList();
            }
            return null;
        }
        private async Task<HashSet<string>> GetPersonIdInvolve(Actor actor)
        {
            int actorType = actor.ActorTypeId;
            IEnumerable<PersonAllin> people = null;
            if (actorType == (int)ActorTypeEnum.Person)
            {
                people = await m_PersonRepositoryN.GetFriends(actor.ActorId);
                return people.Select(p=>p.Person.PersonId).ToHashSet();
            }
            if (actorType == (int)ActorTypeEnum.Page)
            {
                people = await m_PageRepositoryN.GetPersonFollow(actor.ActorId);
                return people.Select(p => p.Person.PersonId).ToHashSet();
            }
            if (actorType == (int)ActorTypeEnum.Club)
            {
                people = await m_ClubRepositoryN.GetMembers(actor.ActorId);
                return people.Select(p => p.Person.PersonId).ToHashSet();
            }
            return null;
        }
        private async Task<HashSet<string>> GetPersonIndustry(List<Industry> industries)
        {
            string[] keySearch = industries.Select(p => p.Code).ToArray();
            List<Person> people = await m_PersonRepositoryM.SearchByIndustries(keySearch);
            return people.Select(p => p.PersonId).ToHashSet();
        }
        private async Task<HashSet<string>> GetPersonSkill(List<Skill> skills)
        {
            if (skills == null) return new HashSet<string>();
            string[] keySearch = skills.Select(p => p.Code).ToArray();
            List<Person> people = await m_PersonRepositoryM.SearchBySkills(keySearch);
            return people.Select(p => p.PersonId).ToHashSet();
        }
        private async Task<HashSet<string>> GetPersonLocation(List<Location> locations)
        {
            string[] keySearch = locations.Select(p => p.Id).ToArray();
            List<Person> people = await m_PersonRepositoryM.SearchByLocations(keySearch);
            return people.Select(p => p.PersonId).ToHashSet();
        }
        private async Task<HashSet<string>> GetPersonDegree(List<Degree> degrees)
        {
            string[] keySearch = degrees.Select(p => p.Value.ToString()).ToArray();
            List<Person> people = await m_PersonRepositoryM.SearchByDegrees(keySearch);
            return people.Select(p => p.PersonId).ToHashSet();
        }
        private async Task<HashSet<string>> GetPersonExperience(int minYear,int maxYear)
        {
            List<Person> people = await m_PersonRepositoryM.SearchByExperience(minYear,maxYear);
            return people.Select(p => p.PersonId).ToHashSet();
        }
        private async Task<string[]> GetPersonSeniority(List<SeniorityLevel> seniorityLevels)
        {

            return null;
        }
        private async Task<string[]> GetPersonEmployeeType(List<EmploymentType> employmentTypes)
        {

            return null;
        }
        public async Task<List<PostAllType>> GetPosts(string personId,int skip)
        {
            List<PostBroadcastPerson> postBroadcastPersons = await m_PostbroadcastPersonRepositoryM.GetByPerson(personId, skip, MAX_LIMIT);
            string[] josbIds = await GetJobPostIdArray(postBroadcastPersons);
            string[] statusIds = await GetStatusPostIdArray(postBroadcastPersons);
            Task<List<JobPost>> jobPostsTask = m_JobPostRepository.Gets(josbIds);
            Task<List<StatusPost>> statusPostsTask = m_StatusPostRepository.Gets(statusIds);
            await Task.WhenAll(jobPostsTask, statusPostsTask);
            List<PostAllType> postAllTypes = Mapper.Map<List<PostAllType>>(jobPostsTask.Result);
            postAllTypes.Concat(Mapper.Map<List<PostAllType>>(statusPostsTask.Result));
            return postAllTypes;
        }
        private async Task<string[]> GetJobPostIdArray(List<PostBroadcastPerson> lstJobPostBroadCast)
        {
            string[] ids = new string[] { };
            if (lstJobPostBroadCast == null) return ids;
            await Task.Run(() => { ids= lstJobPostBroadCast.Where(p => p.PostType == PostTypeEnum.JobPost).Select(p => p.PostId).ToArray(); });
            return ids; 
        }
        private async Task<string[]> GetStatusPostIdArray(List<PostBroadcastPerson> lstStatusPostBroadCast)
        {
            string[] ids = new string[] { };
            if (lstStatusPostBroadCast == null) return ids;
            await Task.Run(() => { ids = lstStatusPostBroadCast.Where(p => p.PostType == PostTypeEnum.StatusPost).Select(p => p.PostId).ToArray(); });
            return ids;
        }

    }
}
