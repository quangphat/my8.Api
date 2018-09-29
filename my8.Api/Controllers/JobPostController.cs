using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my8.Api.IBusiness;
using my8.Api.Infrastructures;
using my8.Api.ISmartCenter;
using my8.Api.Models;
using my8.Api.my8Enum;

namespace my8.Api.Controllers
{
    [Produces("application/json")]
    [Route("JobPosts")]
    public class JobPostController : BaseController
    {
        IJobPostBusiness m_JobPostBusiness;
        IFeedSmart m_FeedSmart;
        public JobPostController(CurrentProcess process, IJobPostBusiness jobpostBusiness, IFeedSmart feedSmart):base(process)
        {
            m_JobPostBusiness = jobpostBusiness;
            m_FeedSmart = feedSmart;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreatePost([FromBody] JobPost model)
        {
            JobPost post = await m_JobPostBusiness.Post(model);
            bool result = false;
            if(post!=null)
            {
                result = await m_FeedSmart.BroadcastToPerson(model);
            }
            return ToResponse(result);
        }
        [HttpGet]
        [Route("{postId}")]
        public async Task<IActionResult> Get(string postId)
        {
            JobPost post = await m_JobPostBusiness.Get(postId);
            return Json(post);
        }
        [HttpPost]
        [Route("getbyauthor")]
        public async Task<IActionResult> GetByAuthor([FromBody] Author author)
        {
            List<JobPost> lstStatusPost = await m_JobPostBusiness.GetByAuthor(author);
            return Json(lstStatusPost);
        }
        [HttpPost]
        [Route("getmultipleJobPost")]
        public async Task<IActionResult> Gets([FromBody] string[] ids)
        {
            List<JobPost> lstStatusPost = await m_JobPostBusiness.Gets(ids);
            return Json(lstStatusPost);
        }
    }
}