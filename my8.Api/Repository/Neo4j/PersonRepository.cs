using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using my8.Api.Infrastructures;
using my8.Api.Interfaces.Neo4j;
using my8.Api.Models.Neo4j;
using Neo4jClient;

namespace my8.Api.Repository.Neo4j
{
    public class PersonRepository : Neo4jRepositoryBase,IPersonRepository
    {
        public PersonRepository(IOptions<Neo4jConnection> settings):base(settings)
        {
        }

        public async Task AddFriend(Person sendBy, Person sendTo)
        {
            await client.Cypher
                .Match("(u1:Person{id:" + sendBy.id +"})", "(u2:Person{id:" + sendTo.id +"})")
                .Merge("(u1)-[r:friend{sendby:" + sendBy.id +",sendDate:'"+DateTime.Today.ToString("yyyy-MM-dd") +"'}]->(u2)")
                .ExecuteWithoutResultsAsync();
        }

        public async Task CreatePerson(Person user)
        {
            await client.Cypher.Create("(e:Person {item})")
                    .WithParam("item", user)
                    .ExecuteWithoutResultsAsync();
        }

        public async Task<IEnumerable<Person>> FindCommonFriend(Person user1, Person user2)
        {
            IEnumerable<Person> results = await client.Cypher
                .OptionalMatch("(u1)-[:friend]-(common)-[:friend]-(u2)")
                .Where((Person u1) => u1.id == user1.id)
                .AndWhere((Person u2) => u2.id == user2.id)
                .Return<Person>("common")
                .ResultsAsync;
            return results;
        }

        public async Task<IEnumerable<PersonAllin>> GetFriends(int Id)
        {
            var allFriend = await client.Cypher
                .Match("(user:Person{id:" + Id + "})-[:friend]-(u:Person)")
                .OptionalMatch("(user)-[:friend]-(common:Person)-[:friend]-(u)")
                .Where("u.id<>1")
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
                .Match("(u1:Person{id:" + currentPerson.id + "})-[f:friend]-(friend:Person)")
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


        public async Task<IEnumerable<PersonAllin>> FindPersons(Person currentUSer,string searchStr)
        {
            IEnumerable<PersonAllin> users = await client.Cypher
                .Match("(u1:Person{id:" + currentUSer.id + "})", "(other: Person)")
                .Where($"Lower(other.DisplayName) contains '{searchStr}'")
                .OptionalMatch("(u1)-[r:friend]-(common:Person)-[:friend]-(other)")
                .Return((other, common) => new PersonAllin
                {
                    Person = other.As<Person>(),
                    CommonFriend = (int)common.Count()
                })
                .ResultsAsync;
            return users;
        }

        public async Task<IEnumerable<Page>> GetFollowedPage(Person user)
        {
            IEnumerable<Page> pages = await client.Cypher
                .OptionalMatch("(u:Person{id:" + user.id + "})-[:Follow]-(p:Page)")
                .Return(p => p.As<Page>())
                .ResultsAsync;
            return pages;
        }

        public async Task<IEnumerable<Club>> GetJoinedClub(Person user)
        {
            IEnumerable<Club> teams = await client.Cypher
                .OptionalMatch("(u:Person{id:" + user.id + "})-[:Join]-(t:Club)")
                .Return(t => t.As<Club>())
                .ResultsAsync;
            return teams;
        }

        public async Task UnFriend(Person currentPerson, Person friend)
        {
            await client.Cypher
                .Match("(u1:Person{id:" + currentPerson.id + "})-[r:friend]-(u2:Person{id:" + friend.id + "})")
                .Delete("r")
                .ExecuteWithoutResultsAsync();
        }

        public async Task<bool> FollowPage(Person currentPerson, Page page)
        {
            try
            {
                await client.Cypher
                .Match("(u:Person{id:" + currentPerson.id + "})", "(p:Page{id:" + page.Id + "})")
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
                .Match("(u:Person{id:" + currentPerson.id + "})-[r:Follow]-(p:Page{id:" + page.Id + "})")
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
                .Match("(u:Person{id:" + currentPerson.id + "})", "(p:Club{id:" + team.Id + "})")
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
                .Match("(u:Person{id:" + currentPerson.id + "})-[r:Join]-(p:Club{id:" + team.Id + "})")
                .Delete("r")
                .ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<PersonAllin> FindParticularPerson(Person currentPerson, Person findingPerson)
        {
            IEnumerable<PersonAllin> users = await client.Cypher
                .Match("(u1:Person{id:" + currentPerson.id + "})", "(u2:Person{id:" + findingPerson.id + "})")
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
                   .Match("(u1:Person{id:" + currentPerson.id + "})", "(u2:Person{id:" + friend.id + "})")
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
                   .Match("(u1:Person{id:" + currentPerson.id + "})", "(p:Page{id:" + page.Id + "})")
               .OptionalMatch("(u1)-[r:Follow]-(p)")
               .Set("r.PPIp = r.PPIp+1").ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }
    }
}
