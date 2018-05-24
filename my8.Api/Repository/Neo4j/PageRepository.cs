
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

        public async Task<bool> Create(Page page)
        {
            try
            {
                await client.Cypher.Create("(p:Page {Id:{Id},DisplayName:{DisplayName},Avatar:{Avatar},Rate:{Rate},Url:{Url},Follows:{Follows},PageIPoint:{PageIPoint},Title:{Title}})")
                    .WithParams(new { Id = page.PageId, DisplayName = page.DisplayName, Avatar = page.Avatar, Rate = page.Rate, Url = page.Url, Follows = page.Follows, PageIPoint = page.PageIPoint, Title = page.Title })
                    .ExecuteWithoutResultsAsync();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public async Task<bool> Update(Page page)
        {
            try
            {
                await client.Cypher
                    .Match($@"(p:Page{{Id:'{page.PageId}'}})")
                    .Set($"p.DisplayName='{page.DisplayName}'")
                    .Set($"p.Rate={page.Rate}")
                    .Set($"p.Avatar='{page.Avatar}'")
                    .Set($"p.PageIPoint={page.PageIPoint}")
                    .Set($"p.Title='{page.Title}'")
                    .ExecuteWithoutResultsAsync();
                return true;
            }
            catch (Exception e)
            { return false; }
        }
        public async Task<PageAllin> Get(string id)
        {
            IEnumerable<PageAllin> pages = await client.Cypher.Match($@"(p:Page{{Id:{id}}}) optional match (p)-[f:Follows]-(u:Person)")
                .Return((p, f) => new PageAllin
                {
                    Page = p.As<Page>(),
                    Follows = (int)f.Count()
                }).Limit(1).ResultsAsync;
            return pages.FirstOrDefault();
        }

        public async Task<IEnumerable<PersonAllin>> GetPersonFollow(string pageId)
        {
            IEnumerable<PersonAllin> result = await client.Cypher
                .Match($@"(p:Page{{Id:{pageId}}}) optional match (p)-[f:Follow]-(u:Person) with u,f")
                .Return((u, f) => new PersonAllin {
                    Person = u.As<Person>(),
                    FollowPage = f.As<FollowEdge>()
                }).ResultsAsync;
            return result;
        }

        public async Task<IEnumerable<Page>> Search(string searchStr,int limit)
        {
            IEnumerable<Page> pages = await client.Cypher
                .Match($"(p:Page) where Lower(p.DisplayName) contains '{searchStr}'")
                .Return(p => p.As<Page>())
                .OrderBy("p.PageIPoint")
                .Limit(limit)
                .ResultsAsync;
            return pages;
        }
        public async Task<IEnumerable<PageAllin>> Search(string searchStr,int skip, int limit)
        {
            IEnumerable<PageAllin> pages = await client.Cypher
                .Match($"(p:Page) where Lower(p.DisplayName) contains '{searchStr}'  with count(p) as Total match(p:Page) where Lower(p.DisplayName) contains '{searchStr}' optional match (p)-[f:Follow]-(u:Person)")
                .Return((p,Total,f)=>new PageAllin {
                    Page = p.As<Model.Page>(),
                    Total = Total.As<int>(),
                    Follows = (int)f.Count()
                })
                .OrderBy("p.PageIPoint")
                .Skip(skip)
                .Limit(limit)
                .ResultsAsync;
            return pages;
        }
    }
}
