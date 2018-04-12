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
namespace my8.Api.Controllers
{
    [Produces("application/json")]
    public class StatusPostController : Controller
    {
        MongoI.IStatusPostRepository statuspostRepositoryM;
        public StatusPostController(MongoI.IStatusPostRepository statuspostRepoM)
        {
            statuspostRepositoryM = statuspostRepoM;
        }
        [HttpPost]
        [Route("api/m-statuspost/create")]
        public async Task CreatePost([FromBody] MongoM.StatusPost post)
        {
            await statuspostRepositoryM.Post(post);
        }
        [HttpPost]
        [Route("api/m-statuspost/getbyactor")]
        public async Task<IActionResult> GetByActor([FromBody] MongoM.Actor actor)
        {
            List<MongoM.StatusPost> lstStatusPost =  await statuspostRepositoryM.GetByAuthor(actor);
            return Json(lstStatusPost);
        }
        [HttpPost]
        [Route("api/m-statuspost/getmultiple-statuspost")]
        public async Task<IActionResult> Gets([FromBody] string[] ids)
        {
            List<MongoM.StatusPost> lstStatusPost = await statuspostRepositoryM.Gets(ids);
            return Json(lstStatusPost);
        }
    }
}