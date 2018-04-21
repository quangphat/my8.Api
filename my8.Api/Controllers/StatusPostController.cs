using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my8.Api.IBusiness;
using my8.Api.Models;
using my8.Api.my8Enum;

namespace my8.Api.Controllers
{
    [Produces("application/json")]
    public class StatusPostController : Controller
    {
        IStatusPostBusiness m_statusPostBusiness;
        IPostBroadcastPersonBusiness m_PostBroadCastToPersonBusiness;
        public StatusPostController(IStatusPostBusiness statusPostBusiness, IPostBroadcastPersonBusiness postBroadcastPersonBusiness)
        {
            m_statusPostBusiness = statusPostBusiness;
            m_PostBroadCastToPersonBusiness = postBroadcastPersonBusiness;
        }
        [HttpPost]
        [Route("api/statuspost/create")]
        public async Task<IActionResult> CreatePost([FromBody] StatusPost model)
        {
            //StatusPost post = null;

            //post = new StatusPost();
            //post.Comments = 10;
            //post.Content = $"The status post #1";
            //post.PostTime = DateTime.Today.ToString("yyyy/MM/dd");
            //post.PostBy = new Actor();
            //post.PostBy.ActorId = "5ad6c5298895ac2a78afd1ac";
            //post.PostBy.DisplayName = "Linh Diệu";
            //post.PostBy.ActorTypeId = (int)ActorTypeEnum.Person;
            StatusPost created = await m_statusPostBusiness.Post(model);

            //StatusPost post = await m_statusPostBusiness.Post(model);
            return Json(created);
        }
        [HttpGet]
        [Route("api/statuspost/get/{postId}")]
        public async Task<IActionResult> Get(string postId)
        {
            StatusPost post = await m_statusPostBusiness.Get(postId);
            return Json(post);
        }
        [HttpPost]
        [Route("api/statuspost/getbyactor")]
        public async Task<IActionResult> GetByActor([FromBody] Actor actor)
        {
            List<StatusPost> lstStatusPost = await m_statusPostBusiness.GetByActor(actor);
            return Json(lstStatusPost);
        }
        [HttpPost]
        [Route("api/statuspost/getmultiple-statuspost")]
        public async Task<IActionResult> Gets([FromBody] string[] ids)
        {
            List<StatusPost> lstStatusPost = await m_statusPostBusiness.Gets(ids);
            return Json(lstStatusPost);
        }
    }
}