using my8.Api.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoI = my8.Api.Interfaces.Mongo;
using NeoI = my8.Api.Interfaces.Neo4j;
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
        public async Task<bool> CreatePage()
        {
            throw new NotImplementedException();
        }
    }
}
