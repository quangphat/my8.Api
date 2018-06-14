﻿using my8.Api.IBusiness;
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
using my8.Api.ISmartCenter;

namespace my8.Api.SmartCenter
{
    public class FeedSmart : IFeedSmart
    {

        MongoI.IPostBroadcastPersonRepository _PostbroadcastPersonRepositoryM;
        MongoI.IPersonRepository _personRepositoryM;
        NeoI.IPersonRepository _personRepositoryN;
        NeoI.IPageRepository _pageRepositoryN;
        NeoI.ICommunityRepository _communityRepositoryN;
        MongoI.IStatusPostRepository _statusPostRepository;
        MongoI.IJobPostRepository _jobPostRepository;
        MongoI.ILastPostBroadCastRepository _lastPostBroadCastRepository;
        public FeedSmart(MongoI.IPostBroadcastPersonRepository postBroadcastPersonRepository
            , NeoI.IPersonRepository personRepositoryN
            , NeoI.IPageRepository pageRepositoryN
            , NeoI.ICommunityRepository communityRepositoryN
            , MongoI.IPersonRepository personRepositoryM
            , MongoI.IStatusPostRepository statusPostRepository
            , MongoI.IJobPostRepository jobPostRepository
            , MongoI.ILastPostBroadCastRepository lastPostBroadCastRepository)
        {
            _PostbroadcastPersonRepositoryM = postBroadcastPersonRepository;
            _personRepositoryN = personRepositoryN;
            _pageRepositoryN = pageRepositoryN;
            _communityRepositoryN = communityRepositoryN;
            _personRepositoryM = personRepositoryM;
            _statusPostRepository = statusPostRepository;
            _jobPostRepository = jobPostRepository;
            _lastPostBroadCastRepository = lastPostBroadCastRepository;
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
        public async Task<List<Feed>> GetPosts(string personId, int skip)
        {
            List<PostBroadcastPerson> postBroadcastPersons = await _PostbroadcastPersonRepositoryM.GetByPerson(personId, skip, Utils.LIMIT_FEED);
            Task<List<JobPost>> jobPostsTask = GetJobPost(postBroadcastPersons);
            Task<List<StatusPost>> statusPostsTask = GetStatusPost(postBroadcastPersons);
            await Task.WhenAll(jobPostsTask, statusPostsTask);
            List<Feed> postAllTypes = Mapper.Map<List<Feed>>(jobPostsTask.Result);
            postAllTypes.AddRange(Mapper.Map<List<Feed>>(statusPostsTask.Result));
            return postAllTypes.OrderByDescending(p => p.PostTime).ToList();
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
                    postBroadcast.ReceiverId = people[i].Person.Id;
                    postBroadcast.PostType = PostTypeEnum.StatusPost;
                    postBroadcast.KeyTime = post.PostTimeUnix;
                    tasks.Add(Task.Run(() =>
                    {
                        _PostbroadcastPersonRepositoryM.Create(postBroadcast);
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
            if (jobPost.Privacy == (int)PostPrivacyEnum.All)
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
            if (jobPost.Privacy == (int)PostPrivacyEnum.All)
            {
                await Task.Run(() =>
                {
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
                for (int i = 0; i < allPersonId.Length; i++)
                {
                    PostBroadcastPerson postBroadcast = new PostBroadcastPerson();
                    postBroadcast.PostId = jobPost.Id;
                    postBroadcast.ReceiverId = allPersonId[i];
                    postBroadcast.PostType = PostTypeEnum.JobPost;
                    postBroadcast.KeyTime = jobPost.PostTimeUnix;
                    lastTasks.Add(Task.Run(() =>
                    {
                        _PostbroadcastPersonRepositoryM.Create(postBroadcast);
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
        private async Task<List<PersonAllin>> GetPersonInvolve(Author author)
        {
            int authorType = author.AuthorTypeId;
            IEnumerable<PersonAllin> people = null;
            if (authorType == (int)AuthorTypeEnum.Person)
            {
                people = await _personRepositoryN.GetFriends(author.AuthorId);
                return people.ToList();
            }
            if (authorType == (int)AuthorTypeEnum.Page)
            {
                people = await _pageRepositoryN.GetPersonFollow(author.AuthorId);
                return people.ToList();
            }
            if (authorType == (int)AuthorTypeEnum.Community)
            {
                people = await _communityRepositoryN.GetMembers(author.AuthorId);
                return people.ToList();
            }
            return null;
        }
        private async Task<HashSet<string>> GetPersonIdInvolve(Author author)
        {
            int authorType = author.AuthorTypeId;
            IEnumerable<PersonAllin> people = null;
            if (authorType == (int)AuthorTypeEnum.Person)
            {
                people = await _personRepositoryN.GetFriends(author.AuthorId);
                return people.Select(p => p.Person.Id).ToHashSet();
            }
            if (authorType == (int)AuthorTypeEnum.Page)
            {
                people = await _pageRepositoryN.GetPersonFollow(author.AuthorId);
                return people.Select(p => p.Person.Id).ToHashSet();
            }
            if (authorType == (int)AuthorTypeEnum.Community)
            {
                people = await _communityRepositoryN.GetMembers(author.AuthorId);
                return people.Select(p => p.Person.Id).ToHashSet();
            }
            return null;
        }
        private async Task<HashSet<string>> GetPersonIndustry(List<Industry> industries)
        {
            if (industries == null) return new HashSet<string>();
            string[] keySearch = industries.Select(p => p.Code).ToArray();
            List<Person> people = await _personRepositoryM.SearchByIndustries(keySearch);
            return people.Select(p => p.PersonId).ToHashSet();
        }
        private async Task<HashSet<string>> GetPersonSkill(List<Skill> skills)
        {
            if (skills == null) return new HashSet<string>();
            string[] keySearch = skills.Select(p => p.Code).ToArray();
            List<Person> people = await _personRepositoryM.SearchBySkills(keySearch);
            return people.Select(p => p.PersonId).ToHashSet();
        }
        private async Task<HashSet<string>> GetPersonLocation(List<Location> locations)
        {
            if (locations == null) return new HashSet<string>();
            string[] keySearch = locations.Select(p => p.Id).ToArray();
            List<Person> people = await _personRepositoryM.SearchByLocations(keySearch);
            return people.Select(p => p.PersonId).ToHashSet();
        }
        private async Task<HashSet<string>> GetPersonDegree(List<Degree> degrees)
        {
            if (degrees == null) return new HashSet<string>();
            List<Person> people = await _personRepositoryM.SearchByDegrees(new string[] { });
            return people.Select(p => p.PersonId).ToHashSet();
        }
        private async Task<HashSet<string>> GetPersonExperience(int minYear, int maxYear)
        {
            List<Person> people = await _personRepositoryM.SearchByExperience(minYear, maxYear);
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

        private async Task<List<JobPost>> GetJobPost(List<PostBroadcastPerson> postBroadcastPeople)
        {
            string[] josbIds = await GetJobPostIdArray(postBroadcastPeople);
            return await _jobPostRepository.Gets(josbIds);
        }
        private async Task<List<StatusPost>> GetStatusPost(List<PostBroadcastPerson> postBroadcastPeople)
        {
            string[] statusIds = await GetStatusPostIdArray(postBroadcastPeople);
            return await _statusPostRepository.Gets(statusIds);
        }
        private async Task<string[]> GetJobPostIdArray(List<PostBroadcastPerson> lstJobPostBroadCast)
        {
            string[] ids = new string[] { };
            if (lstJobPostBroadCast == null) return ids;
            await Task.Run(() => { ids = lstJobPostBroadCast.Where(p => p.PostType == PostTypeEnum.JobPost).OrderBy(p => p.KeyTime).Select(p => p.PostId).ToArray(); });
            return ids;
        }
        private async Task<string[]> GetStatusPostIdArray(List<PostBroadcastPerson> lstStatusPostBroadCast)
        {
            string[] ids = new string[] { };
            if (lstStatusPostBroadCast == null) return ids;
            await Task.Run(() => { ids = lstStatusPostBroadCast.Where(p => p.PostType == PostTypeEnum.StatusPost).OrderBy(p => p.KeyTime).Select(p => p.PostId).ToArray(); });
            return ids;
        }

        public Task<List<Feed>> InjectGrayMatter(List<PostBroadcastPerson> postBroadcasts, List<Feed> postAllTypes)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InitBroadcast(string personId)
        {
            List<Task> tasks = new List<Task>();
            Task<IEnumerable<Page>> getFollowingPagesTask = _personRepositoryN.GetFollowingPage(personId);
            Task<IEnumerable<Community>> getCommunityTask = _personRepositoryN.GetJoiningCommunitys(personId);
            await Task.WhenAll(getFollowingPagesTask, getCommunityTask);
            List<Author> authors = new List<Author>();
            if(getFollowingPagesTask.Result!=null)
            {
                authors.AddRange(Mapper.Map<List<Author>>(getFollowingPagesTask.Result));
            }
            if(getCommunityTask.Result!=null)
            {
                authors.AddRange(Mapper.Map<List<Author>>(getCommunityTask.Result));
            }
            bool result = false;
            if (authors != null)
            {
                LastPostBroadCast lastPost = null;
                PostBroadcastPerson postBroadcastPerson = null;
                for (int i = 0; i < authors.Count(); i++)
                {
                    lastPost = null;
                    if(authors[i].AuthorTypeId == (int)AuthorTypeEnum.Page)
                    {
                        lastPost = await _lastPostBroadCastRepository.GetByPageId(authors[i].AuthorId);
                    }
                    else if(authors[i].AuthorTypeId == (int)AuthorTypeEnum.Community)
                    {
                        lastPost = await _lastPostBroadCastRepository.GetByCommunityId(authors[i].AuthorId);
                    }
                    long lastPostTimeUnix = 0;
                    if (lastPost != null)
                    {
                        lastPostTimeUnix = lastPost.LastPostTimeToPerson;
                    }
                    Task<List<StatusPost>> getstatusPostsTask = _statusPostRepository.GetByAuthor(authors[i], 0, Utils.LIMIT_FEED, lastPostTimeUnix);
                    Task<List<JobPost>> getJobPostTask = _jobPostRepository.GetByAuthor(authors[i], 0, Utils.LIMIT_FEED, lastPostTimeUnix);
                    await Task.WhenAll(getstatusPostsTask, getJobPostTask);
                    List<Feed> feeds = new List<Feed>();
                    if (getstatusPostsTask.Result != null)
                        feeds.AddRange(Mapper.Map<List<Feed>>(getstatusPostsTask.Result));
                    if (getJobPostTask.Result != null)
                        feeds.AddRange(Mapper.Map<List<Feed>>(getJobPostTask.Result));
                    foreach (Feed feed in feeds)
                    {
                        postBroadcastPerson = new PostBroadcastPerson
                        {
                            PostId = feed.Id,
                            KeyTime = feed.PostTimeUnix,
                            Like = false,
                            PostType = (PostTypeEnum)feed.PostType,
                            ReceiverId = personId
                        };
                        result = await _PostbroadcastPersonRepositoryM.Create(postBroadcastPerson);
                    }
                    Feed lastFeed = feeds.OrderByDescending(p => p.PostTimeUnix).FirstOrDefault();
                    if(lastFeed!=null)
                    {
                        if (lastPost == null)
                        {
                            lastPost = new LastPostBroadCast
                            {
                                AuthorId = lastFeed.PostBy.AuthorId,
                                LastPostIdToPerson = lastFeed.Id,
                                LastPostTimeToPerson = lastFeed.PostTimeUnix,
                                PersonId = personId,
                                AuthorType = (AuthorTypeEnum)lastFeed.PostBy.AuthorTypeId
                            };
                            await _lastPostBroadCastRepository.Create(lastPost);
                        }
                        else
                        {
                            lastPost.LastPostIdToPerson = lastFeed.Id;
                            lastPost.LastPostTimeToPerson = lastFeed.PostTimeUnix;
                            lastPost.AuthorId = lastFeed.PostBy.AuthorId;
                            await _lastPostBroadCastRepository.Update(lastPost);
                        }
                    }
                }
            }
            return result;
        }
    }
}
