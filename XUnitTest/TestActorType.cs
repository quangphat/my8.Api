using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using XUnitTest.Libs;
using my8.Api.my8Enum;
using my8.Api.Models;

namespace XUnitTest
{
    public class TestAuthorType : IClassFixture<Server>
    {
        private readonly Server server;
        public TestAuthorType(Server _server)
        {
            server = _server;
        }
        [Fact]
        public async Task TestCreateAuthorType()
        {
            AuthorType authorType = new AuthorType();
            authorType.Name = "person";
            authorType.Value = (int)AuthorTypeEnum.Person;
            await server.Call(HttpMethod.Post, "/api/m-authortype-create",authorType, (rp) =>
            {
                Assert.NotNull(rp);
                Assert.NotNull(rp.Content);
                Assert.True(rp.IsSuccessStatusCode);

                var body = rp.Content.ReadAsStringAsync().Result;

                Assert.NotNull(body);
            });
        }
        [Fact]
        public async Task TestGetAuthorType()
        {
            await server.Call(HttpMethod.Get, "/api/m-authortype-create", (rp) =>
            {
                Assert.NotNull(rp);
                Assert.NotNull(rp.Content);
                Assert.True(rp.IsSuccessStatusCode);

                var body = rp.Content.ReadAsStringAsync().Result;

                Assert.NotNull(body);
            });
        }
        [Fact]
        public async Task TestUpdateAuthorType()
        {
            await server.Call(HttpMethod.Put, "/api/m-authortype-create", (rp) =>
            {
                Assert.NotNull(rp);
                Assert.NotNull(rp.Content);
                Assert.True(rp.IsSuccessStatusCode);

                var body = rp.Content.ReadAsStringAsync().Result;

                Assert.NotNull(body);
            });
        }
        [Fact]
        public async Task TestDeleteAuthorType()
        {
            await server.Call(HttpMethod.Delete, "/api/m-authortype-create", (rp) =>
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
