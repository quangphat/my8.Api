﻿
using Microsoft.Extensions.Options;
using my8.Api.Infrastructures;
using my8.Api.Interfaces.Neo4j;
using my8.Api.Models;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model = my8.Api.Models;
namespace my8.Api.Repository.Neo4j
{
    public class PageRepository : Neo4jRepositoryBase,IPageRepository
    {
        public PageRepository(IOptions<Neo4jConnection> setting):base(setting)
        {
        }
        public Task<int> CountFollow(Page page)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Create(Page page)
        {
            try
            {
                await client.Cypher.Create("(p:Page {item})")
                    .WithParam("item", page)
                    .ExecuteWithoutResultsAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Page> Get(string id)
        {
            IEnumerable<Page> pages = await client.Cypher
                .Match("(p:Page{Id:{id}})")
                .WithParam("id", id)
                .Return(p => p.As<Page>())
                .Limit(1)
                .ResultsAsync;
            return pages.FirstOrDefault();
        }

        public async Task<IEnumerable<PersonAllin>> GetPersonFollow(string pageId)
        {
            IEnumerable<PersonAllin> result = await client.Cypher
                .Match("(p:Page{Id:{id}})")
                .WithParams(new { id = pageId })
                .OptionalMatch("(p)-[f:Follow]-(u:Person)")
                .With("u,f")
                .Return((u, f) => new PersonAllin {
                    Person = u.As<Person>(),
                    FollowPage = f.As<FollowEdge>()
                }).ResultsAsync;
            return result;
        }

        public async Task<IEnumerable<Page>> Search(string searchStr,int limit)
        {
            IEnumerable<Page> pages = await client.Cypher
                .Match("(p:Page)")
                .Where($"Lower(p.DisplayName) contains '{searchStr}'")
                .Return(p => p.As<Page>())
                .OrderBy("p.PageIPoint")
                .Limit(limit)
                .ResultsAsync;
            return pages;
        }
        public async Task<IEnumerable<PageAllin>> Search(string searchStr,int skip, int limit)
        {
            IEnumerable<PageAllin> pages = await client.Cypher
                .Match("(p:Page) with count(p) as Total")
                .Match("(p:Page) with p,Total")
                .Where($"Lower(p.DisplayName) contains '{searchStr}'")
                .Return((p,Total)=>new PageAllin {
                    Page = p.As<Model.Page>(),
                    Total = Total.As<int>()
                })
                .OrderBy("p.PageIPoint")
                .Skip(skip)
                .Limit(limit)
                .ResultsAsync;
            return pages;
        }
    }
}
