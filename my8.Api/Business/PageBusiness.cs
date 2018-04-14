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
        MongoI.IPageRepository m_pageRepositoryM;
        NeoI.IPageRepository m_pageRepositoryN;
        public PageBusiness(MongoI.IPageRepository pageRepoM, NeoI.IPageRepository pageRepoN)
        {
            m_pageRepositoryM = pageRepoM;
            m_pageRepositoryN = pageRepoN;
        }
        public async Task<Page> Create(Page page)
        {
            string result = await m_pageRepositoryM.Create(page);
            if (string.IsNullOrWhiteSpace(result))
                return null;
            page.PageId = result;
            bool t2 = await m_pageRepositoryN.Create(page);
            if(t2)
            {
                return page;
            }
            return null;
        }
        public async Task<bool> Update(Page page)
        {
            Task<bool> result1 = m_pageRepositoryN.Update(page);
            Task<bool> result2 = m_pageRepositoryM.Update(page);
            await Task.WhenAll(result1, result2);
            if (result1.Result && result2.Result)
                return true;
            return false;
        }
        public async Task<PageAllin> Get(string id)
        {
            PageAllin page = await m_pageRepositoryN.Get(id);
            if (page != null)
                return page;
            return null;
        }

        public async Task<List<PersonAllin>> GetPeopleFollow(string pageId)
        {
            IEnumerable<PersonAllin> people = await m_pageRepositoryN.GetPersonFollow(pageId);
            return people.ToList();
        }

        public async Task<List<PageAllin>> Search(string searchStr, int skip, int limit)
        {
            IEnumerable<PageAllin> pages = await m_pageRepositoryN.Search(searchStr, skip, limit);
            return pages.ToList();
        }
    }
}
