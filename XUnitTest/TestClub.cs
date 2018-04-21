
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using XUnitTest.Libs;

namespace XUnitTest
{
    public class TestCommunity : IClassFixture<Server>
    {
        private readonly Server server;
        public TestCommunity(Server _server)
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
    }
}
