using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using my8.Api;
using my8.Api.Infrastructures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTest.Libs
{
    public class Server : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public Server()
        {
            var collection = new ServiceCollection();
            collection.AddOptions();
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();
            var config = builder.Build();

            collection.Configure<MongoConnection>(config.GetSection("MongoConnection"));
            collection.Configure<Neo4jConnection>(config.GetSection("Neo4jConnection"));
            var services = collection.BuildServiceProvider();
            var options = services.GetService<IOptions<MongoConnection>>();
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            var webBuilder = new WebHostBuilder()
           .UseContentRoot(Directory.GetCurrentDirectory()).UseConfiguration(builder.Build())
           .UseStartup<Startup>();
            _server = new TestServer(webBuilder);


            //Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "staging");
            //_server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }
        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }

        public async Task Call(HttpMethod method, string path, Action<HttpResponseMessage> action)
        {
            await Call(method, path, null, action);
        }
        public async Task Call(HttpMethod method, string path, object body, Action<HttpResponseMessage> action)
        {
            var originalData = string.Empty;
            if (method == HttpMethod.Get)
            {
                var list = new List<string>();

                if (path.Contains("?"))
                    foreach (var q in path.Split('?')[1].Split('&'))
                        if (q.Contains("="))
                            list.Add(q.Split('=')[1]);

                originalData = string.Join(string.Empty, list);
            }
            else if (body != null)
                originalData = JsonConvert.SerializeObject(body);

            if (string.IsNullOrWhiteSpace(originalData))
                originalData = string.Empty;

            var request = new HttpRequestMessage(method, path);

            //request.Headers.Add("Accept", "application/json");
            if (body != null)
                if (body is string)
                    request.Content = new StringContent(body as string, Encoding.UTF8, "application/json");
                else
                    request.Content = new StringContent(originalData, Encoding.UTF8, "application/json");
            using (var rp = await _client.SendAsync(request))
                action(rp);
        }

    }
}
