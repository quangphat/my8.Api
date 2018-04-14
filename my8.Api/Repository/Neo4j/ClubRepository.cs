using Microsoft.Extensions.Options;
using my8.Api.Infrastructures;
using my8.Api.Interfaces.Neo4j;
using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Repository.Neo4j
{
    public class ClubRepository:Neo4jRepositoryBase,IClubRepository
    {
        public ClubRepository(IOptions<Neo4jConnection> setting) : base(setting) { }

        public async Task<bool> Create(Club club)
        {
            try
            {
                await client.Cypher.Create("(c:Club {Id:{Id},DisplayName:{DisplayName},Avatar:{Avatar},Rate:{Rate},Joins:{Joins},ClubIPoint:{ClubIPoint},Title:{Title}})")
                    .WithParams(new { Id = club.ClubId, club.DisplayName, club.Avatar, club.Rate, club.Joins, club.ClubIPoint, club.Title })
                    .ExecuteWithoutResultsAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<bool> Update(Club club)
        {
            try
            {
                await client.Cypher
                    .Match("(c:Club{Id:'" + club.ClubId + "'})")
                    .Set($"c.DisplayName='{club.DisplayName}'")
                    .Set($"c.Rate={club.Rate}")
                    .Set($"c.Avatar='{club.Avatar}'")
                    .Set($"c.ClubIPoint={club.ClubIPoint}")
                    .Set($"c.Title='{club.Title}'")
                    .ExecuteWithoutResultsAsync();
                return true;
            }
            catch(Exception e)
            { return false; }
        }
        public async Task<ClubAllin> Get(string id)
        {
            IEnumerable<ClubAllin> clubs = await client.Cypher.Match("(c:Club{Id:{id}})")
                .WithParam("id", id)
                .OptionalMatch("(c)-[j:Join]-(p:Person)")
                .Return((c,j)=>new ClubAllin {
                    Club = c.As<Club>(),
                    Joins = (int)j.Count()
                }).Limit(1).ResultsAsync;
            return clubs.FirstOrDefault();
        }
        public async Task<bool> AddMember(string clubId, string personId)
        {
            try
            {
                await client.Cypher
                .Match("(u:Person{Id:'" + personId + "'})", "(c:Club{Id:'" + clubId + "'})")
                .Create("(u)-[:Join{PCIp:0}]->(c)")
                .ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }
        public async Task<IEnumerable<PersonAllin>> GetMembers(string clubId)
        {
            IEnumerable<PersonAllin> result = await client.Cypher
                .Match("(c:Club{Id:{id}})")
                .WithParams(new { id = clubId })
                .OptionalMatch("(c)-[j:Join]-(u:Person)")
                .With("u,j")
                .Return((u, j) => new PersonAllin
                {
                    Person = u.As<Person>(),
                    JoinClub = j.As<JoinEdge>()
                }).ResultsAsync;
            return result;
        }

        public async Task<bool> KickOutMember(string clubId, string personId)
        {
            try
            {
                await client.Cypher.Match("(c:Club{Id:'" + clubId + "'}),(p:Person{Id:'" + personId + "'})")
                    .OptionalMatch("(c)-[j:Join]-(p)")
                    .Delete("j")
                    .ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }
        public async Task<IEnumerable<ClubAllin>> Search(string searchStr, int skip, int limit)
        {
            IEnumerable<ClubAllin> clubs = await client.Cypher
                .Match("(c:Club)")
                .Where($"Lower(c.DisplayName) contains '{searchStr}'  with count(c) as Total ")
                .Match($"(c:Page) where Lower(c.DisplayName) contains '{searchStr}'")
                .OptionalMatch("(c)-[j:Join]-(u:Person)")
                .Return((c, Total, j) => new ClubAllin
                {
                    Club = c.As<Club>(),
                    Total = Total.As<int>(),
                    Joins = (int)j.Count()
                })
                .OrderBy("c.ClubIPoint")
                .Skip(skip)
                .Limit(limit)
                .ResultsAsync;
            return clubs;
        }

        
    }
}

