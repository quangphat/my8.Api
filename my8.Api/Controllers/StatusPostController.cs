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
using my8.Api.SmartCenter;

namespace my8.Api.Controllers
{
    [Produces("application/json")]
    public class StatusPostController : BaseController
    {
        IStatusPostBusiness m_statusPostBusiness;
        IPostBroadcastPersonBusiness m_PostBroadCastToPersonBusiness;
        ISmartCenter m_SmartCenter;
        public StatusPostController(CurrentProcess process, ISmartCenter smartCenter,IStatusPostBusiness statusPostBusiness):base(process)
        {
            m_SmartCenter = smartCenter;
            m_statusPostBusiness = statusPostBusiness;
        }
        [HttpPost]
        [Route("api/StatusPost/create")]
        public async Task<IActionResult> CreatePost([FromBody] StatusPost model)
        {
            StatusPost post = await m_statusPostBusiness.Post(model);
            bool result = false;
            if(post!=null)
            {
                result = await m_SmartCenter.BroadcastToPerson(model);
            }
            return ToResponse(result);
        }
        [HttpGet]
        [Route("api/statuspost/get/{postId}")]
        public async Task<IActionResult> Get(string postId)
        {
            StatusPost post = await m_statusPostBusiness.Get(postId);
            return Json(post);
        }
        [HttpPost]
        [Route("api/statuspost/getbyauthor")]
        public async Task<IActionResult> GetByAuthor([FromBody] ShortPerson author)
        {
            List<StatusPost> lstStatusPost = await m_statusPostBusiness.GetByAuthor(author);
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