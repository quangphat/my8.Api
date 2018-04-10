
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using XUnitTest.Libs;

namespace XUnitTest
{
    public class TestPage : IClassFixture<Server>
    {
        private readonly Server server;
        public TestPage(Server _server)
        {
            server = _server;
        }
        [Fact]
        public async Task Test()
        {
            await server.Call(HttpMethod.Get, "/api/page/search/liv", (rp) =>
            {
                Assert.NotNull(rp);
                Assert.NotNull(rp.Content);
                Assert.True(rp.IsSuccessStatusCode);

                var body = rp.Content.ReadAsStringAsync().Result;

                Assert.NotNull(body);
            });
        }
        [Fact]
        public async Task Test_GetPageFollers()
        {
            await server.Call(HttpMethod.Get, "/api/n/page/get-person-follow/2", (rp) =>
            {
                Assert.NotNull(rp);
                Assert.NotNull(rp.Content);
                Assert.True(rp.IsSuccessStatusCode);

                var body = rp.Content.ReadAsStringAsync().Result;

                Assert.NotNull(body);
            });
        }
        [Fact]
        public async Task Test_SearchPage()
        {
            await server.Call(HttpMethod.Get, "/api/n/page/search/Page/30/30", (rp) =>
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
