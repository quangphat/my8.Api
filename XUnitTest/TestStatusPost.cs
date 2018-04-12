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
            post.Images = null;
            post.Active = true;
            post.Privacy = (int)PostPrivaryEnum.All;
            post.Content = "Today is Sunday";
            post.EmploymentType = null;
            post.Seniority = null;
            Actor user = new Actor();
            user.DisplayName = "Quang Phát";
            user.ActorId = "5ac9be056272224af07b79d3";
            user.ActorTypeId = (int)PostAuthorTypeEnum.Person;
            post.PostBy = user;
            await server.Call(HttpMethod.Post, "/api/m-statuspost/create", post, (rp) =>
            {
                Assert.NotNull(rp);
                Assert.NotNull(rp.Content);
                Assert.True(rp.IsSuccessStatusCode);

                var body = rp.Content.ReadAsStringAsync().Result;

                Assert.NotNull(body);
            });
        }
        [Fact]
        public async Task Test_GetStatusPostByActor()
        {
            Actor user = new Actor();
            user.DisplayName = "Quang Phát";
            user.ActorId = "5ac9be056272224af07b79d3";
            user.ActorTypeId = (int)PostAuthorTypeEnum.Person;
            await server.Call(HttpMethod.Post, "/api/m-statuspost/getbyactor", user, (rp) =>
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
            string[] ids = new string[] {"5ac9d4c150f2f84ba07f1c6a", "5aca1312b326542c24b7c67f" };
            await server.Call(HttpMethod.Post, "/api/m-statuspost/getmultiple-statuspost", ids, (rp) =>
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
