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
        public StatusPostController(IStatusPostBusiness statusPostBusiness,IPostBroadcastPersonBusiness postBroadcastPersonBusiness)
        {
            m_statusPostBusiness = statusPostBusiness;
            m_PostBroadCastToPersonBusiness = postBroadcastPersonBusiness;
        }
        [HttpPost]
        [Route("api/statuspost/create")]
        public async Task<IActionResult> CreatePost([FromBody] StatusPost model)
        {
            StatusPost post = null;
            for(int i=1002;i<1102;i++)
            {
                post = new StatusPost();
                post.Active = true;
                post.Comments = 10;
                post.Content = $"The status post #{i}";
                post.PostTime = DateTime.Today.ToString("yyyy/MM/dd");
                post.PostBy = new Actor();
                post.PostBy.ActorId = "5acedf96c86324070424f263";
                post.PostBy.DisplayName = "Quang Phát";
                post.PostBy.ActorTypeId = (int)ActorTypeEnum.Person;
                StatusPost created = await m_statusPostBusiness.Post(post);
                if(created!=null)
                {
                    await m_PostBroadCastToPersonBusiness.BroadcastToPerson(created);
                }
            }

            //StatusPost post = await m_statusPostBusiness.Post(model);
            return Json(true);
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