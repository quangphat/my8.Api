using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my8.Api.IBusiness;
using my8.Api.Infrastructures;
using my8.Api.Models;
using my8.Api.my8Enum;
using my8.Api.ISmartCenter;

namespace my8.Api.Controllers
{
    [Produces("application/json")]
    [Route("StatusPosts")]
    public class StatusPostController : BaseController
    {
        IStatusPostBusiness m_statusPostBusiness;
        IPostBroadcastPersonBusiness m_PostBroadCastToPersonBusiness;
        IFeedSmart m_FeedSmart;
        public StatusPostController(CurrentProcess process, IFeedSmart feedSmart,IStatusPostBusiness statusPostBusiness):base(process)
        {
            m_FeedSmart = feedSmart;
            m_statusPostBusiness = statusPostBusiness;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreatePost([FromBody] StatusPost model)
        {
            StatusPost post = await m_statusPostBusiness.Post(model);
            bool result = false;
            if (post != null)
            {
                result = await m_FeedSmart.BroadcastToPerson(model);
            }
            return ToResponse(result);
        }
        [HttpGet]
        [Route("{postId}")]
        public async Task<IActionResult> Get(string postId)
        {
            StatusPost post = await m_statusPostBusiness.Get(postId);
            return Json(post);
        }
        [HttpPost]
        [Route("getbyauthor")]
        public async Task<IActionResult> GetByAuthor([FromBody] Author author)
        {
            List<StatusPost> lstStatusPost = await m_statusPostBusiness.GetByAuthor(author);
            return Json(lstStatusPost);
        }
        [HttpPost]
        [Route("multipleStatuspost")]
        public async Task<IActionResult> Gets([FromBody] string[] ids)
        {
            List<StatusPost> lstStatusPost = await m_statusPostBusiness.Gets(ids);
            return Json(lstStatusPost);
        }
    }
}