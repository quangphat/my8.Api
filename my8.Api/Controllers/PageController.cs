using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoM = my8.Api.Models;
using MongoI = my8.Api.Interfaces.Mongo;
using NeoI = my8.Api.Interfaces.Neo4j;
using SqlI = my8.Api.Interfaces.Sql;
using my8.Api.IBusiness;
using my8.Api.Models;

namespace my8.Api.Controllers
{
    [Produces("application/json")]
    public class PageController : Controller
    {
        MongoI.IPageRepository pageRepositoryM;
        NeoI.IPageRepository pageRepositoryN;
        IPageBusiness m_pageBusiness;
        public PageController(IPageBusiness pageBusiness,MongoI.IPageRepository pageRepoM,NeoI.IPageRepository pageRepoN)
        {
            pageRepositoryM = pageRepoM;
            pageRepositoryN = pageRepoN;
            m_pageBusiness = pageBusiness;
        }
        [HttpPost]
        [Route("api/page/create")]
        public async Task<IActionResult> Create([FromBody] Page page)
        {
            Page result = await m_pageBusiness.Create(page);
            return Json(result);
        }

        [HttpGet]
        [Route("api/page/get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Page page = await m_pageBusiness.Get(id);
            return Json(page);
        }
        [HttpGet]
        [Route("api/n/page/search/{searchStr}/{skip}/{limit}")]
        public async Task<IActionResult> Search(string searchStr,int skip,int limit)
        {
            List<PageAllin> lstPage = await m_pageBusiness.Search(searchStr, skip, limit);
            return Json(lstPage);
        }
    }
}