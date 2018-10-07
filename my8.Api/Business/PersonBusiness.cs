using my8.Api.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoI = my8.Api.Interfaces.Mongo;
using NeoI = my8.Api.Interfaces.Neo4j;
using SqlI = my8.Api.Interfaces.Sql;
using AutoMapper;
using my8.Api.Models;
using my8.Api.Infrastructures;

namespace my8.Api.Business
{
    public class PersonBusiness :BaseBusiness, IPersonBusiness
    {
        const int TOP = 10;
        MongoI.IPersonRepository m_personRepositoryM;
        NeoI.IPersonRepository m_personRepositoryN;
        SqlI.IPersonRepository m_personRepositoryS;
        public PersonBusiness(MongoI.IPersonRepository personRepoM, 
            NeoI.IPersonRepository personRepoN, 
            SqlI.IPersonRepository personRepositoryS,CurrentProcess process):base(process)
        {
            m_personRepositoryM = personRepoM;
            m_personRepositoryN = personRepoN;
            m_personRepositoryS = personRepositoryS;
        }

        public async Task<Person> Create(Person person)
        {
            person.IndustriesCode = person.JobFunctionTags != null ? person.JobFunctionTags.Select(p => p.Code).ToArray() : null;
            person.SkillsCode = person.SkillTags != null ? person.SkillTags.Select(p => p.Code).ToArray() : null;
            string rs1= await m_personRepositoryM.Create(person);
            if (string.IsNullOrWhiteSpace(rs1)) return null;
            person.PersonId = rs1;
            bool rs2 = await m_personRepositoryN.Create(person);
            bool rs3 = await m_personRepositoryS.Create(person);
            if (rs2 && rs3)
                return person;
            return null;
        }

        public async Task<Person> Get(string id)
        {
            Person person = await m_personRepositoryM.Get(id);
            return person;
        }

        public async Task<Person> GetSql(string id)
        {
            Person person = await m_personRepositoryS.Get(id);
            return person;
        }

        public async Task<List<PersonAllin>> Search(string currentPersonId,string searchStr, int skip, int limit)
        {
            IEnumerable<PersonAllin> people = await m_personRepositoryN.Search(currentPersonId, searchStr, skip, limit);
            return people.ToList();
        }
        public async Task<bool> Update(Person person)
        {
            Task<bool> t1 = m_personRepositoryM.Update(person);
            Task<bool> t2 = m_personRepositoryN.Update(person);
            Task<bool> t3 = m_personRepositoryS.Update(person);
            await Task.WhenAll(t1, t2, t3);
            if (t1.Result && t2.Result && t3.Result)
                return true;
            return false;
        }
        public async Task<bool> InteractToFriend(Person current, Person friend)
        {
            return await m_personRepositoryN.InteractionToFriend(current, friend);
        }
        public async Task<List<Person>> FindCommondFriend(string currentId, string friendId)
        {
            IEnumerable<Person> commons = await m_personRepositoryN.FindCommonFriend(currentId, friendId);
            return commons.ToList();
        }
        public async Task<bool> AddFriend(string sendBy, string sendTo)
        {
            return await m_personRepositoryN.AddFriend(sendBy, sendTo);
        }
        public async Task<bool> UnFriend(string sendBy, string sendTo)
        {
            return await m_personRepositoryN.UnFriend(sendBy, sendTo);
        }
        public async Task<List<PersonAllin>> GetAllFriend(string personId)
        {
            IEnumerable<PersonAllin> friends = await m_personRepositoryN.GetFriends(personId);
            return friends.ToList();
        }
        public async Task<List<PersonAllin>> GetTopFriendInteractive(string personId)
        {
            IEnumerable<PersonAllin> friends = await m_personRepositoryN.GetTopFriendInteractive(personId, TOP);
            return friends.ToList();
        }
        public async Task<List<Page>> GetFollowingPage(string personId)
        {
            IEnumerable<Page> pages = await m_personRepositoryN.GetFollowingPage(personId);
            return pages.ToList();
        }
        public async Task<bool> FollowPage(string currentPersonId, string pageId)
        {
            return await m_personRepositoryN.FollowPage(currentPersonId, pageId);
        }
        public async Task<bool> UnFollowPage(string currentPersonId, string pageId)
        {
            return await m_personRepositoryN.UnFollowPage(currentPersonId, pageId);
        }
        public async Task<bool> InteractToPage(string currentPersonId, string pageId)
        {
            return await m_personRepositoryN.InteractToPage(currentPersonId, pageId);
        }

        public async Task<List<Community>> GetJoiningCommunitys(string personId)
        {
            IEnumerable<Community> lstCommunity = await m_personRepositoryN.GetJoiningCommunitys(personId);
            return lstCommunity.ToList();
        }
        public async Task<bool> JoinCommunity(string currentPersonId, string CommunityId)
        {
            return await m_personRepositoryN.JoinCommunity(currentPersonId, CommunityId);
        }
        public async Task<bool> OutCommunity(string currentPersonId, string CommunityId)
        {
            return await m_personRepositoryN.OutCommunity(currentPersonId, CommunityId);
        }
        public async Task<bool> InteractToCommunity(string currentPersonId, string CommunityId)
        {
            return await m_personRepositoryN.InteractToCommunity(currentPersonId, CommunityId);
        }

        public async Task<Person> Login(Person model)
        {
            if (model == null) return null;
            Person person = await m_personRepositoryS.Login(model.Email,model.Password);
            return person;
        }

        public async Task<List<Page>> GetRecommendPage(string personId, int limit)
        {
            IEnumerable<Page> pages = await m_personRepositoryN.GetRecommendPage(personId, limit);
            return pages.ToList();
        }

        public async Task<Person> GetByUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return null;
            url = url.Trim().ToLower();
            return await m_personRepositoryM.GetByUrl(url);
        }
    }
}
