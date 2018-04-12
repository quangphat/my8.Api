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
                    .WithParams(new { Id = club.ClubId, DisplayName = club.DisplayName, Avatar = club.Avatar, Rate = club.Rate, Joins = club.Joins, ClubIPoint = club.ClubIPoint, Title = club.Title })
                    .ExecuteWithoutResultsAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<Club> Get(string id)
        {
            IEnumerable<Club> clubs = await client.Cypher.Match("(c:Club{Id:{id}})")
                .WithParam("id", id)
                .Return(c => c.As<Club>()).Limit(1).ResultsAsync;
            return clubs.FirstOrDefault();
        }
        public async Task<int> CountMember(Club team)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Person>> GetMembers(Club team)
        {
            throw new NotImplementedException();
        }

        public Task KickOutMember(Club team, Person user)
        {
            throw new NotImplementedException();
        }
    }
}

