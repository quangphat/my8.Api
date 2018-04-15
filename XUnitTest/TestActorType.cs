using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using XUnitTest.Libs;
using my8.Api.my8Enum;
using my8.Api.Models;

namespace XUnitTest
{
    public class TestActorType : IClassFixture<Server>
    {
        private readonly Server server;
        public TestActorType(Server _server)
        {
            server = _server;
        }
        [Fact]
        public async Task TestCreateActorType()
        {
            ActorType actorType = new ActorType();
            actorType.Name = "person";
            actorType.Value = (int)ActorTypeEnum.Person;
            await server.Call(HttpMethod.Post, "/api/m-actortype-create",actorType, (rp) =>
            {
                Assert.NotNull(rp);
                Assert.NotNull(rp.Content);
                Assert.True(rp.IsSuccessStatusCode);

                var body = rp.Content.ReadAsStringAsync().Result;

                Assert.NotNull(body);
            });
        }
        [Fact]
        public async Task TestGetActorType()
        {
            await server.Call(HttpMethod.Get, "/api/m-actortype-create", (rp) =>
            {
                Assert.NotNull(rp);
                Assert.NotNull(rp.Content);
                Assert.True(rp.IsSuccessStatusCode);

                var body = rp.Content.ReadAsStringAsync().Result;

                Assert.NotNull(body);
            });
        }
        [Fact]
        public async Task TestUpdateActorType()
        {
            await server.Call(HttpMethod.Put, "/api/m-actortype-create", (rp) =>
            {
                Assert.NotNull(rp);
                Assert.NotNull(rp.Content);
                Assert.True(rp.IsSuccessStatusCode);

                var body = rp.Content.ReadAsStringAsync().Result;

                Assert.NotNull(body);
            });
        }
        [Fact]
        public async Task TestDeleteActorType()
        {
            await server.Call(HttpMethod.Delete, "/api/m-actortype-create", (rp) =>
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
