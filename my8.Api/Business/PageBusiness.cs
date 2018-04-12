using my8.Api.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoI = my8.Api.Interfaces.Mongo;
using NeoI = my8.Api.Interfaces.Neo4j;
using AutoMapper;
using my8.Api.Models;

namespace my8.Api.Business
{
    public class PageBusiness : IPageBusiness
    {
        MongoI.IPageRepository pageRepositoryM;
        NeoI.IPageRepository pageRepositoryN;
        public PageBusiness(MongoI.IPageRepository pageRepoM, NeoI.IPageRepository pageRepoN)
        {
            pageRepositoryM = pageRepoM;
            pageRepositoryN = pageRepoN;
        }
        public async Task<Page> Create(Page page)
        {
            string result = await pageRepositoryM.Create(page);
            if (string.IsNullOrWhiteSpace(result))
                return null;
            page.Id = result;
            bool t2 = await pageRepositoryN.Create(page);
            if(t2)
            {
                return page;
            }
            return null;
        }

        public async Task<Page> Get(string id)
        {
            Page page = await pageRepositoryN.Get(id);
            if (page != null)
                return page;
            return null;
        }

        public async Task<List<PersonAllin>> GetPeopleFollow(string pageId)
        {
            IEnumerable<PersonAllin> people = await pageRepositoryN.GetPersonFollow(pageId);
            return people.ToList();
        }

        public async Task<List<PageAllin>> Search(string searchStr, int skip, int limit)
        {
            IEnumerable<PageAllin> pages = await pageRepositoryN.Search(searchStr, skip, limit);
            return pages.ToList();
        }
    }
}
