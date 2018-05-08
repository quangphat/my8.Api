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
        NeoI.ICommunityRepository m_CommunityRepositoryN;
        MongoI.IStatusPostRepository m_StatusPostRepository;
        MongoI.IJobPostRepository m_JobPostRepository;
        public SmartCenter(MongoI.IPostBroadcastPersonRepository postBroadcastPersonRepository
            ,NeoI.IPersonRepository personRepositoryN
            ,NeoI.IPageRepository pageRepositoryN
            ,NeoI.ICommunityRepository CommunityRepositoryN
            ,MongoI.IPersonRepository personRepositoryM
            ,MongoI.IStatusPostRepository statusPostRepository
            ,MongoI.IJobPostRepository jobPostRepository)
        {
            m_PostbroadcastPersonRepositoryM = postBroadcastPersonRepository;
            m_PersonRepositoryN = personRepositoryN;
            m_PageRepositoryN = pageRepositoryN;
            m_CommunityRepositoryN = CommunityRepositoryN;
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
            Task<HashSet<string>> lstPersonByAuthor = GetPersonIdInvolve(jobPost.PostBy);
            tasks.Add(lstPersonByAuthor);
            Task<HashSet<string>> lstPersonByIndustry = null;
            Task<HashSet<string>> lstPersonBySkill = null;
            Task<HashSet<string>> lstPersonByExperience = null;
            if (jobPost.Privacy==(int)PostPrivacyEnum.All)
            {
                //must satisfy 
                lstPersonByIndustry = GetPersonIndustry(jobPost.IndustryTags);
                tasks.Add(lstPersonByIndustry);
                //must satisfy 
                lstPersonBySkill = GetPersonSkill(jobPost.SkillTags);
                tasks.Add(lstPersonBySkill);
                // must satisfy
                 lstPersonByExperience = GetPersonExperience(jobPost.MinExperience, jobPost.MaxExperience);
                tasks.Add(lstPersonByExperience);
            }
           
            //optional
            //Task<HashSet<string>> lstPersonByLocation = GetPersonLocation(jobPost.Locations);
            //tasks.Add(lstPersonByLocation);
            //optional
            //Task<HashSet<string>> lstPersonByDegree = GetPersonDegree(jobPost.Degrees);
            //tasks.Add(lstPersonByDegree);
            
            await Task.WhenAll(tasks);
            List<HashSet<string>> hashSetsMustSatisfy = new List<HashSet<string>>();
            if(jobPost.Privacy==(int)PostPrivacyEnum.All)
            {
                await Task.Run(() => {
                    //must satisfy 
                    hashSetsMustSatisfy.Add(lstPersonByIndustry.Result);
                    hashSetsMustSatisfy.Add(lstPersonBySkill.Result);
                    hashSetsMustSatisfy.Add(lstPersonByExperience.Result);
                    string[] allPersonSatisfyJob = Utils.IntersectOrUnion(hashSetsMustSatisfy);

                    string[] allPersonByPost = lstPersonByAuthor.Result.ToArray();

                    //hashSetsMustSatisfy.Add(lstPersonByLocation.Result);
                    //hashSetsMustSatisfy.Add(lstPersonByDegree.Result);

                    allPersonId = allPersonSatisfyJob.Union(allPersonByPost).ToArray();
                });
            }
            else
            {
                allPersonId = lstPersonByAuthor.Result.ToArray();
            }
            
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
        private async Task<List<PersonAllin>> GetPersonInvolve(ShortPerson author)
        {
            int authorType = author.AuthorTypeId;
            IEnumerable<PersonAllin> people = null;
            if (authorType == (int)AuthorTypeEnum.Person)
            {
                people = await m_PersonRepositoryN.GetFriends(author.AuthorId);
                return people.ToList();
            }
            if (authorType == (int)AuthorTypeEnum.Page)
            {
                people = await m_PageRepositoryN.GetPersonFollow(author.AuthorId);
                return people.ToList();
            }
            if (authorType == (int)AuthorTypeEnum.Community)
            {
                people = await m_CommunityRepositoryN.GetMembers(author.AuthorId);
                return people.ToList();
            }
            return null;
        }
        private async Task<HashSet<string>> GetPersonIdInvolve(ShortPerson author)
        {
            int authorType = author.AuthorTypeId;
            IEnumerable<PersonAllin> people = null;
            if (authorType == (int)AuthorTypeEnum.Person)
            {
                people = await m_PersonRepositoryN.GetFriends(author.AuthorId);
                return people.Select(p=>p.Person.PersonId).ToHashSet();
            }
            if (authorType == (int)AuthorTypeEnum.Page)
            {
                people = await m_PageRepositoryN.GetPersonFollow(author.AuthorId);
                return people.Select(p => p.Person.PersonId).ToHashSet();
            }
            if (authorType == (int)AuthorTypeEnum.Community)
            {
                people = await m_CommunityRepositoryN.GetMembers(author.AuthorId);
                return people.Select(p => p.Person.PersonId).ToHashSet();
            }
            return null;
        }
        private async Task<HashSet<string>> GetPersonIndustry(List<Industry> industries)
        {
            if (industries == null) return new HashSet<string>();
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
            if (locations == null) return new HashSet<string>();
            string[] keySearch = locations.Select(p => p.Id).ToArray();
            List<Person> people = await m_PersonRepositoryM.SearchByLocations(keySearch);
            return people.Select(p => p.PersonId).ToHashSet();
        }
        private async Task<HashSet<string>> GetPersonDegree(List<Degree> degrees)
        {
            if (degrees == null) return new HashSet<string>();
            List<Person> people = await m_PersonRepositoryM.SearchByDegrees(new string[] { });
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

        public Task<List<PostAllType>> InjectGrayMatter(List<PostBroadcastPerson> postBroadcasts, List<PostAllType> postAllTypes)
        {
            throw new NotImplementedException();
        }
    }
}
