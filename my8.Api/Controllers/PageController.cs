using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoM = my8.Api.Models.Mongo;
using MongoI = my8.Api.Interfaces.Mongo;
using NeoM = my8.Api.Models.Neo4j;
using NeoI = my8.Api.Interfaces.Neo4j;
using SqlM = my8.Api.Models.Sql;
using SqlI = my8.Api.Interfaces.Sql;
namespace my8.Api.Controllers
{
    [Produces("application/json")]
    public class PageController : Controller
    {
        MongoI.IPageRepository pageRepositoryM;
        NeoI.IPageRepository pageRepositoryN;
        public PageController(MongoI.IPageRepository pageRepoM,NeoI.IPageRepository pageRepoN)
        {
            pageRepositoryM = pageRepoM;
            pageRepositoryN = pageRepoN;
        }

        #region Mongo
        [HttpPost]
        [Route("api/m/page/create-page")]
        public async Task<IActionResult> CreatePage([FromBody] MongoM.Page model)
        {
            bool success = await pageRepositoryM.Create(model);
            return Json(success);
        }
        [HttpGet]
        [Route("api/m/page/getpagebyid/{id}")]
        public async Task<IActionResult> GetPage(string id)
        {
            MongoM.Page page = await pageRepositoryM.Get(id);
            return Json(page);
        }
        #endregion

        #region neo
        [HttpPost]
        [Route("api/n/page/create-page")]
        public async Task<IActionResult> CreatePageInNeo([FromBody] NeoM.Page model)
        {
            bool success = await pageRepositoryN.Create(model);
            return Json(success);
        }
        [HttpGet]
        [Route("api/n/page/get-person-follow/{id}")]
        public async Task<IActionResult> GetPersonFollows(int id)
        {
            IEnumerable<NeoM.PersonAllin> lstPerson = await pageRepositoryN.GetPersonFollow(id);
            return Json(lstPerson);
        }
        [HttpGet]
        [Route("api/n/page/search/{searchStr}/{skip}/{limit}")]
        public async Task<IActionResult> SearchPage(string searchStr,int skip,int limit)
        {
            IEnumerable<NeoM.PageAllin> lstPage = await pageRepositoryN.Search(searchStr.ToLower(),skip,limit);
            return Json(lstPage);
        }
        #endregion
    }
}