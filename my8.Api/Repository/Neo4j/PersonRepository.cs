﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using my8.Api.Infrastructures;
using my8.Api.Interfaces.Neo4j;
using Model = my8.Api.Models;
using Neo4jClient;
using my8.Api.Models;

namespace my8.Api.Repository.Neo4j
{
    public class PersonRepository : Neo4jRepositoryBase,IPersonRepository
    {
        public PersonRepository(IOptions<Neo4jConnection> settings):base(settings)
        {
        }
        public async Task<bool> Create(Person user)
        {
            try
            {
                await client.Cypher.Create("(e:Person {Id:{Id},DisplayName:{DisplayName},WorkAs:{WorkAs},Company:{Company},Rate:{Rate},Avatar:{Avatar}})")
                    .WithParams(new { Id = user.PersonId, DisplayName = user.DisplayName, WorkAs = user.WorkAs, Company = user.Company, Rate = user.Rate, Avatar = user.Avatar })
                    .ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }

        }
        public async Task<bool> Update(Person person)
        {
            try
            {
                await client.Cypher
                    .Match("(p:Person{Id:'" + person.PersonId + "'})")
                    .Set($"p.DisplayName='{person.DisplayName}'")
                    .Set($"p.WorkAs='{person.WorkAs}'")
                    .Set($"p.Company='{person.Company}'")
                    .Set($"p.Rate={person.Rate}")
                    .Set($"p.Avatar='{person.Avatar}'")
                    .ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }
        public async Task<bool> AddFriend(string sendBy, string sendTo)
        {
            try
            {
                await client.Cypher
                .Match("(u1:Person{Id:'" + sendBy + "'})", "(u2:Person{Id:'" + sendTo + "'})")
                .Merge("(u1)-[r:Friend{Sendby:'" + sendBy + "',SendDate:'" + DateTime.Today.ToString("yyyy/MM/dd") + "',IPp:0}]->(u2)")
                .ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }
        public async Task<bool> UnFriend(string sendBy, string sendTo)
        {
            try
            {
                await client.Cypher
                .Match("(u1:Person{Id:'" + sendBy + "'})", "(u2:Person{Id:'" + sendTo + "'})")
                .OptionalMatch("(u1)-[r:Friend]->(u2)")
                .Delete("r")
                .ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }
        

        public async Task<IEnumerable<Person>> FindCommonFriend(string p1Id, string friendId)
        {
            IEnumerable<Person> results = await client.Cypher
                .OptionalMatch("(u1)-[:Friend]-(common)-[:Friend]-(u2)")
                .Where((Person u1) => u1.PersonId == p1Id)
                .AndWhere((Person u2) => u2.PersonId == friendId)
                .Return<Person>("common")
                .ResultsAsync;
            return results;
        }

        public async Task<IEnumerable<PersonAllin>> GetFriends(string personId)
        {
            var allFriend = await client.Cypher
                .Match("(user:Person{Id:'" + personId + "'})-[:Friend]-(u:Person)")
                .OptionalMatch("(user)-[:Friend]-(common:Person)-[:Friend]-(u)")
                .Where($"u.Id<>'{personId}'")
                .Return((u, common) => new PersonAllin
                {
                    Person = u.As<Person>(),
                    CommonFriend = (int)common.Count()
                })
                .ResultsAsync;
            return allFriend;
        }


        public async Task<IEnumerable<PersonAllin>> GetTopFriendInteractive(Person currentPerson, int top)
        {
            IEnumerable<PersonAllin> lstPerson = await client.Cypher
                .Match("(u1:Person{Id:" + currentPerson.PersonId + "})-[f:Friend]-(friend:Person)")
                .Return((friend, f) => new PersonAllin
                {
                    Person = friend.As<Person>(),
                    Friend = f.As<FriendEdge>()
                })
                .OrderByDescending("f.interactive")
                .Limit(top)
                .ResultsAsync;
            return lstPerson;
        }


        public async Task<IEnumerable<PersonAllin>> Search(string currentPersonId, string searchStr,int skip,int limit)
        {
            IEnumerable<PersonAllin> users = await client.Cypher
                .Match("(other: Person)")
                .Where($"Lower(other.DisplayName) contains '{searchStr}' and other.Id<>'{currentPersonId}'  with count(other) as Total ")
                .Match("(u1:Person{Id:'" + currentPersonId + "'}),(other:Person) where Lower(other.DisplayName) contains '" + searchStr +"' and other.Id<>'" +currentPersonId +"' optional match(u1)-[r:Friend]-(common:Person)-[:Friend]-(other) ")
                .Return((other, common,Total) => new PersonAllin
                {
                    Person = other.As<Person>(),
                    CommonFriend = (int)common.Count(),
                    Total = Total.As<int>()
                })
                .OrderBy("other.Rate")
                .Skip(skip)
                .Limit(limit)
                .ResultsAsync;
            return users;
        }

        public async Task<PersonAllin> FindParticularPerson(Person currentPerson, Person findingPerson)
        {
            IEnumerable<PersonAllin> users = await client.Cypher
                .Match("(u1:Person{Id:" + currentPerson.PersonId + "})", "(u2:Person{Id:" + findingPerson.PersonId + "})")
                .OptionalMatch("(u1)-[r:Friend]-(common:Person)-[:Friend]-(u2)")
                .Return((u2, common) => new PersonAllin
                {
                    Person = u2.As<Person>(),
                    CommonFriend = (int)common.Count()
                })
                .ResultsAsync;
            return users.FirstOrDefault();
        }

        public async Task<bool> InteractionToFriend(Person currentPerson, Person friend)
        {
            try
            {
                await client.Cypher
                   .Match("(u1:Person{Id:" + currentPerson.PersonId + "})", "(u2:Person{Id:" + friend.PersonId + "})")
               .OptionalMatch("(u1)-[r:Friend]-(u2)")
               .Set("r.interactive = r.interactive+1").ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }
        public async Task<IEnumerable<Page>> GetFollowingPage(string userId)
        {
            IEnumerable< Page> pages = await client.Cypher
                .OptionalMatch("(u:Person{Id:'" + userId + "'})-[:Follow]-(p:Page)")
                .Return(p => p.As<Page>())
                .ResultsAsync;
            return pages;
        }
        public async Task<bool> FollowPage(string currentPersonId, string pageId)
        {
            try
            {
                await client.Cypher
                .Match("(u:Person{Id:'" + currentPersonId + "'})", "(p:Page{Id:'" + pageId + "'})")
                .Create("(u)-[:Follow{PPIp:0}]->(p)")
                .ExecuteWithoutResultsAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UnFollowPage(string currentPersonId, string pageId)
        {
            try
            {
                await client.Cypher
                .Match("(u:Person{Id:'" + currentPersonId + "'})-[r:Follow]-(p:Page{Id:'" + pageId + "'})")
                .Delete("r")
                .ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }
        public async Task<bool> InteractToPage(string currentPersonId, string pageId)
        {
            try
            {
                await client.Cypher
                   .Match("(u1:Person{Id:'" + currentPersonId + "'})", "(p:Page{Id:'" + pageId + "'})")
               .OptionalMatch("(u1)-[r:Follow]-(p)")
               .Set("r.PPIp = r.PPIp+1").ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }
        public async Task<IEnumerable<Community>> GetJoiningCommunitys(string userId)
        {
            IEnumerable<Community> teams = await client.Cypher
                .OptionalMatch("(u:Person{Id:'" +userId + "'})-[:Join]-(t:Community)")
                .Return(t => t.As<Community>())
                .ResultsAsync;
            return teams;
        }


        public async Task<bool> JoinCommunity(string currentPersonID, string CommunityId)
        {
            try
            {
                await client.Cypher
                .Match("(u:Person{Id:'" + currentPersonID + "'})", "(c:Community{Id:'" + CommunityId + "'})")
                .Create("(u)-[:Join{PCIp:0}]->(c)")
                .ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> OutCommunity(string currentPersonId, string CommunityId)
        {
            try
            {
                await client.Cypher
                .Match("(u:Person{Id:'" + currentPersonId + "'})-[r:Join]-(p:Community{Id:'" + CommunityId + "'})")
                .Delete("r")
                .ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }
        public async Task<bool> InteractToCommunity(string currentPersonId, string CommunityId)
        {
            try
            {
                await client.Cypher
                   .Match("(u1:Person{Id:'" + currentPersonId + "'})", "(c:Community{Id:'" + CommunityId + "'})")
               .OptionalMatch("(u1)-[r:Join]-(c)")
               .Set("r.PCIp = r.PCIp+1").ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }

    }
}
