﻿
using my8.Api.Models;
using my8.Api.my8Enum;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using XUnitTest.Libs;

namespace XUnitTest
{
    public class TestStatusPost : IClassFixture<Server>
    {
        private readonly Server server;
        public TestStatusPost(Server _server)
        {
            server = _server;
        }
        [Fact]
        public async Task Test_CreateStatusPost()
        {
            StatusPost post = new StatusPost();
            post.Comments = 4;
            post.Likes = 10;
            post.Shares = 1;
            post.Views = 15;
            post.PostTime = DateTime.UtcNow;
            post.PostTimeUnix = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            post.Images = null;
            post.Privacy = (int)PostPrivacyType.All;
            post.Content = "Yeah";
            post.PostBy = new Author()
            {
                DisplayName = "",
                AuthorId = "5ad063f571db6e14c4681aa9",
                AuthorTypeId = (int)my8.Api.my8Enum.AuthorType.Page
            };

            await server.Call(HttpMethod.Post, "/api/StatusPost/create", post, (rp) =>
                {
                    Assert.NotNull(rp);
                    Assert.NotNull(rp.Content);
                    Assert.True(rp.IsSuccessStatusCode);

                    var body = rp.Content.ReadAsStringAsync().Result;

                    Assert.NotNull(body);
                });
        }
        [Fact]
        public async Task Test_GetStatusPostByAuthor()
        {
            Author user = new Author();
            user.DisplayName = "Quang Phát";
            user.AuthorId = "5ac9be056272224af07b79d3";
            user.AuthorTypeId = (int)my8.Api.my8Enum.AuthorType.Person;
            await server.Call(HttpMethod.Post, "/api/m-statuspost/getbyauthor", user, (rp) =>
            {
                Assert.NotNull(rp);
                Assert.NotNull(rp.Content);
                Assert.True(rp.IsSuccessStatusCode);

                var body = rp.Content.ReadAsStringAsync().Result;

                Assert.NotNull(body);
            });
        }
        [Fact]
        public async Task Test_GetMultipleStatusPost()
        {
            string[] ids = new string[] { "5ac9d4c150f2f84ba07f1c6a", "5aca1312b326542c24b7c67f" };
            await server.Call(HttpMethod.Post, "/api/m-statuspost/getmultiple-statuspost", ids, (rp) =>
            {
                Assert.NotNull(rp);
                Assert.NotNull(rp.Content);
                Assert.True(rp.IsSuccessStatusCode);

                var body = rp.Content.ReadAsStringAsync().Result;

                Assert.NotNull(body);
            });
        }
        [Fact]
        public async Task Test_PostBroadCast()
        {
            await server.Call(HttpMethod.Post, "/api/Feed/Test/5acedf96c86324070424f263", null, (rp) =>
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
