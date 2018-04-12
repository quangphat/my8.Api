using System;
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

        public async Task<bool> AddFriend(Person sendBy, Person sendTo)
        {
            try
            {
                await client.Cypher
                .Match("(u1:Person{Id:" + sendBy.Id + "})", "(u2:Person{Id:" + sendTo.Id + "})")
                .Merge("(u1)-[r:Friend{Sendby:" + sendBy.Id + ",SendDate:'" + DateTime.Today.ToString("yyyy-MM-dd") + "'}]->(u2)")
                .ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> Create(Person user)
        {
            try
            {
                await client.Cypher.Create("(e:Person {item})")
                    .WithParam("item", user)
                    .ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
            
        }

        public async Task<IEnumerable<Person>> FindCommonFriend(Person user1, Person user2)
        {
            IEnumerable<Person> results = await client.Cypher
                .OptionalMatch("(u1)-[:friend]-(common)-[:friend]-(u2)")
                .Where((Person u1) => u1.Id == user1.Id)
                .AndWhere((Person u2) => u2.Id == user2.Id)
                .Return<Person>("common")
                .ResultsAsync;
            return results;
        }

        public async Task<IEnumerable<PersonAllin>> GetFriends(string Id)
        {
            var allFriend = await client.Cypher
                .Match("(user:Person{Id:" + Id + "})-[:friend]-(u:Person)")
                .OptionalMatch("(user)-[:friend]-(common:Person)-[:friend]-(u)")
                .Where($"u.Id<>{Id}")
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
                .Match("(u1:Person{Id:" + currentPerson.Id + "})-[f:friend]-(friend:Person)")
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


        public async Task<IEnumerable<PersonAllin>> FindPersons(Person currentUSer,string searchStr,int skip,int limit)
        {
            IEnumerable<PersonAllin> users = await client.Cypher
                .Match("(u1:Person{Id:" + currentUSer.Id + "})", "(other: Person)")
                .Where($"Lower(other.DisplayName) contains '{searchStr}'")
                .OptionalMatch("(u1)-[r:friend]-(common:Person)-[:friend]-(other)")
                .Return((other, common) => new PersonAllin
                {
                    Person = other.As<Person>(),
                    CommonFriend = (int)common.Count()
                })
                .OrderBy("other.Rate")
                .Skip(skip)
                .Limit(limit)
                .ResultsAsync;
            return users;
        }

        public async Task<IEnumerable<Page>> GetFollowingPage(Person user)
        {
            IEnumerable< Page> pages = await client.Cypher
                .OptionalMatch("(u:Person{Id:" + user.Id + "})-[:Follow]-(p:Page)")
                .Return(p => p.As<Page>())
                .ResultsAsync;
            return pages;
        }

        public async Task<IEnumerable<Club>> GetJoinedClub(Person user)
        {
            IEnumerable<Club> teams = await client.Cypher
                .OptionalMatch("(u:Person{Id:" + user.Id + "})-[:Join]-(t:Club)")
                .Return(t => t.As<Club>())
                .ResultsAsync;
            return teams;
        }

        public async Task UnFriend(Person currentPerson, Person friend)
        {
            await client.Cypher
                .Match("(u1:Person{Id:" + currentPerson.Id + "})-[r:friend]-(u2:Person{Id:" + friend.Id + "})")
                .Delete("r")
                .ExecuteWithoutResultsAsync();
        }

        public async Task<bool> FollowPage(Person currentPerson, Page page)
        {
            try
            {
                await client.Cypher
                .Match("(u:Person{Id:" + currentPerson.Id + "})", "(p:Page{Id:" + page.Id + "})")
                .Create("(u)-[:Follow]->(p)")
                .ExecuteWithoutResultsAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UnFollowPage(Person currentPerson, Page page)
        {
            try
            {
                await client.Cypher
                .Match("(u:Person{Id:" + currentPerson.Id + "})-[r:Follow]-(p:Page{Id:" + page.Id + "})")
                .Delete("r")
                .ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> JoinClub(Person currentPerson, Club team)
        {
            try
            {
                await client.Cypher
                .Match("(u:Person{Id:" + currentPerson.Id + "})", "(p:Club{Id:" + team.Id + "})")
                .Create("(u)-[:Join]->(p)")
                .ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> OutClub(Person currentPerson, Club team)
        {
            try
            {
                await client.Cypher
                .Match("(u:Person{Id:" + currentPerson.Id + "})-[r:Join]-(p:Club{Id:" + team.Id + "})")
                .Delete("r")
                .ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<PersonAllin> FindParticularPerson(Person currentPerson, Person findingPerson)
        {
            IEnumerable<PersonAllin> users = await client.Cypher
                .Match("(u1:Person{Id:" + currentPerson.Id + "})", "(u2:Person{Id:" + findingPerson.Id + "})")
                .OptionalMatch("(u1)-[r:friend]-(common:Person)-[:friend]-(u2)")
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
                   .Match("(u1:Person{Id:" + currentPerson.Id + "})", "(u2:Person{Id:" + friend.Id + "})")
               .OptionalMatch("(u1)-[r:friend]-(u2)")
               .Set("r.interactive = r.interactive+1").ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> InteractToPage(Person currentPerson, Page page)
        {
            try
            {
                await client.Cypher
                   .Match("(u1:Person{Id:" + currentPerson.Id + "})", "(p:Page{Id:" + page.Id + "})")
               .OptionalMatch("(u1)-[r:Follow]-(p)")
               .Set("r.PPIp = r.PPIp+1").ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> Update(Person person)
        {
            try
            {
                await client.Cypher
                    .Match("(p:Person{Id:" + person.Id + "})")
                    .Set($"p.DisplayName={person.DisplayName}")
                    .Set($"p.WorkAs={person.WorkAs}")
                    .Set($"p.Company={person.Company}")
                    .Set($"p.Rate={person.Rate}")
                    .Set($"p.Avatar={person.Avatar}")
                    .ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }
    }
}
