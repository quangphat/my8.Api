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
    public class CommunityRepository:Neo4jRepositoryBase,ICommunityRepository
    {
        public CommunityRepository(IOptions<Neo4jConnection> setting) : base(setting) { }

        public async Task<bool> Create(Community Community)
        {
            try
            {
                await client.Cypher.Create("(c:Community {Id:{Id},DisplayName:{DisplayName},Avatar:{Avatar},Rate:{Rate},Joins:{Joins},CommunityIPoint:{CommunityIPoint},Title:{Title}})")
                    .WithParams(new { Id = Community.CommunityId, Community.DisplayName, Community.Avatar, Community.Rate, Community.Joins, Community.CommunityIPoint, Community.Title })
                    .ExecuteWithoutResultsAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<bool> Update(Community Community)
        {
            try
            {
                await client.Cypher
                    .Match($@"(c:Community{{Id:'{Community.CommunityId}'}})")
                    .Set($"c.DisplayName='{Community.DisplayName}'")
                    .Set($"c.Rate={Community.Rate}")
                    .Set($"c.Avatar='{Community.Avatar}'")
                    .Set($"c.CommunityIPoint={Community.CommunityIPoint}")
                    .Set($"c.Title='{Community.Title}'")
                    .ExecuteWithoutResultsAsync();
                return true;
            }
            catch(Exception e)
            { return false; }
        }
        public async Task<CommunityAllin> Get(string id)
        {
            IEnumerable<CommunityAllin> Communitys = await client.Cypher.Match($@"(c:Community{{Id:{id}}}) optional match (c)-[j:Join]-(p:Person)")
                .Return((c,j)=>new CommunityAllin {
                    Community = c.As<Community>(),
                    Joins = (int)j.Count()
                }).Limit(1).ResultsAsync;
            return Communitys.FirstOrDefault();
        }
        public async Task<bool> AddMember(string CommunityId, string personId)
        {
            try
            {
                await client.Cypher
                .Match($@"(u:Person{{Id:'{personId}'}}),(c:Community{{Id:'{CommunityId}'}}) create (u)-[:Join{{PCIp:0}}]->(c)")
                .ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }
        public async Task<IEnumerable<PersonAllin>> GetMembers(string communityId)
        {
            IEnumerable<PersonAllin> result = await client.Cypher
                .Match($@"(c:Community{{Id:{{{communityId}}}) optional match (c)-[j:Join]-(u:Person) with u,j")
                .Return((u, j) => new PersonAllin
                {
                    Person = u.As<Person>(),
                    JoinCommunity = j.As<JoinEdge>()
                }).ResultsAsync;
            return result;
        }

        public async Task<bool> KickOutMember(string CommunityId, string personId)
        {
            try
            {
                await client.Cypher.Match($@"(c:Community{{Id:'{CommunityId}'}}),(p:Person{{Id:'{personId}'}}) optional match (c)-[j:Join]-(p) delete j")
                    .ExecuteWithoutResultsAsync();
                return true;
            }
            catch { return false; }
        }
        public async Task<IEnumerable<CommunityAllin>> Search(string searchStr, int skip, int limit)
        {
            IEnumerable<CommunityAllin> Communitys = await client.Cypher
                .Match($"(c:Community) where Lower(c.DisplayName) contains '{searchStr}'  with count(c) as Total match (c:Page) where Lower(c.DisplayName) contains '{searchStr}' optional match (c)-[j:Join]-(u:Person)")
                .Return((c, Total, j) => new CommunityAllin
                {
                    Community = c.As<Community>(),
                    Total = Total.As<int>(),
                    Joins = (int)j.Count()
                })
                .OrderBy("c.CommunityIPoint")
                .Skip(skip)
                .Limit(limit)
                .ResultsAsync;
            return Communitys;
        }

        
    }
}

