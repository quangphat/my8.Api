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
using my8.Api.Models;

namespace my8.Api.Business
{
    public class PersonBusiness : IPersonBusiness
    {
        MongoI.IPersonRepository m_personRepositoryM;
        NeoI.IPersonRepository m_personRepositoryN;
        SqlI.IPersonRepository m_personRepositoryS;
        public PersonBusiness(MongoI.IPersonRepository personRepoM, NeoI.IPersonRepository personRepoN, SqlI.IPersonRepository personRepositoryS)
        {
            m_personRepositoryM = personRepoM;
            m_personRepositoryN = personRepoN;
            m_personRepositoryS = personRepositoryS;
        }

        public async Task<Person> Create(Person person)
        {
            string rs1= await m_personRepositoryM.Create(person);
            if (string.IsNullOrWhiteSpace(rs1)) return null;
            person.Id = rs1;
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

        public async Task<List<PersonAllin>> Search(Person current,string searchStr, int skip, int limit)
        {
            IEnumerable<PersonAllin> people = await m_personRepositoryN.FindPersons(current, searchStr, skip, limit);
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
        public async Task<List<Person>> FindCommondFriend(Person current, Person friend)
        {
            IEnumerable<Person> commons = await m_personRepositoryN.FindCommonFriend(current, friend);
            return commons.ToList();
        }
        public async Task<bool> AddFriend(Person sendBy, Person sendTo)
        {
            return await m_personRepositoryN.AddFriend(sendBy, sendTo);
        }
        public async Task<List<PersonAllin>> GetAllFriend(string personId)
        {
            IEnumerable<PersonAllin> friends = await m_personRepositoryN.GetFriends(personId);
            return friends.ToList();
        }
        public async Task<List<PersonAllin>> GetTopFriendInteractive(Person currentPerson, int top)
        {
            IEnumerable<PersonAllin> friends = await m_personRepositoryN.GetTopFriendInteractive(currentPerson, top);
            return friends.ToList();
        }
        public async Task<List<Page>> GetFollowingPage(Person person)
        {
            IEnumerable<Page> pages = await m_personRepositoryN.GetFollowingPage(person);
            return pages.ToList();
        }
        public async Task<bool> FollowPage(Person currentPerson, Page page)
        {
            return await m_personRepositoryN.FollowPage(currentPerson, page);
        }
        public async Task<bool> UnFollowPage(Person currentPerson, Page page)
        {
            return await m_personRepositoryN.UnFollowPage(currentPerson,page);
        }
        public async Task<bool> InteractToPage(Person currentPerson, Page page)
        {
            return await m_personRepositoryN.InteractToPage(currentPerson, page);
        }

    }
}
