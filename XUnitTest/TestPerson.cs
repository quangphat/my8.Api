using MongoM = my8.Api.Models.Mongo;
using NeoM = my8.Api.Models.Neo4j;
using my8.Api.my8Enum;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using XUnitTest.Libs;

namespace XUnitTest
{
    public class TestPerson : IClassFixture<Server>
    {
        private readonly Server server;
        public TestPerson(Server _server)
        {
            server = _server;
        }

        [Fact]
        public async Task Test_CreatePerson()
        {
            MongoM.Person person = new MongoM.Person();
            person.DisplayName = "Quang Phát";
            person.Rate = 9;
            person.WorkAs = "Developer";
            
            await server.Call(HttpMethod.Post, "/api/m-person/create-person", person, (rp) =>
            {
                Assert.NotNull(rp);
                Assert.NotNull(rp.Content);
                Assert.True(rp.IsSuccessStatusCode);

                var body = rp.Content.ReadAsStringAsync().Result;

                Assert.NotNull(body);
            });
        }
        [Fact]
        public async Task Test_UpdatePerson()
        {
            MongoM.Person person = new MongoM.Person();
            person.Id = "5ac9be056272224af07b79d3";
            person.DisplayName = "Quang Phát";
            person.Rate = 10;
            person.WorkAs = "Developer";

            await server.Call(HttpMethod.Put, "/api/m-person/update", person, (rp) =>
            {
                Assert.NotNull(rp);
                Assert.NotNull(rp.Content);
                Assert.True(rp.IsSuccessStatusCode);

                var body = rp.Content.ReadAsStringAsync().Result;

                Assert.NotNull(body);
            });
        }
        [Fact]
        public async Task Test_GetPersonById()
        {
            await server.Call(HttpMethod.Get, "/api/m-person/5ac9be056272224af07b79d3", (rp) =>
            {
                Assert.NotNull(rp);
                Assert.NotNull(rp.Content);
                Assert.True(rp.IsSuccessStatusCode);

                var body = rp.Content.ReadAsStringAsync().Result;

                Assert.NotNull(body);
            });
        }
        [Fact]
        public async Task Test_PersonInteractionToFriend()
        {
            List<NeoM.Person> people = new List<NeoM.Person>();
            NeoM.Person current = new NeoM.Person();
            current.id = 1;
            NeoM.Person friend = new NeoM.Person();
            friend.id = 2;
            people.Add(current);
            people.Add(friend);
            await server.Call(HttpMethod.Post, "/api/n-person/interactiontofriend",people, (rp) =>
            {
                Assert.NotNull(rp);
                Assert.NotNull(rp.Content);
                Assert.True(rp.IsSuccessStatusCode);

                var body = rp.Content.ReadAsStringAsync().Result;

                Assert.NotNull(body);
            });
        }
    }
}
