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
    public class JobPostController : Controller
    {
        IJobPostBusiness m_JobPostBusiness;
        IPostBroadcastPersonBusiness m_PostBroadCastToPersonBusiness;
        public JobPostController(IJobPostBusiness jobpostBusiness, IPostBroadcastPersonBusiness postBroadcastPersonBusiness)
        {
            m_JobPostBusiness = jobpostBusiness;
            m_PostBroadCastToPersonBusiness = postBroadcastPersonBusiness;
        }
        [HttpPost]
        [Route("api/JobPost/create")]
        public async Task<IActionResult> CreatePost([FromBody] JobPost model)
        {
            JobPost post = null;
            bool result = false;
            for (int i = 0; i < 200; i++)
            {
                post = new JobPost();
                post.Active = true;
                post.Comments = 10;
                post.Content = $"The job post #{i}";
                post.PostTime = DateTime.Today.ToString("yyyy/MM/dd");
                post.PostBy = new Actor();
                post.PostBy.ActorId = "5acedf96c86324070424f263";
                post.PostBy.DisplayName = "Quang Phát";
                post.PostBy.ActorTypeId = (int)ActorTypeEnum.Person;
                JobPost created = await m_JobPostBusiness.Post(post);
                if (created != null)
                {
                   result = await m_PostBroadCastToPersonBusiness.BroadcastToPerson(created);
                }
            }

            //JobPost post = await m_JobPostBusiness.Post(model);
            return Json(result);
        }
        [HttpGet]
        [Route("api/JobPost/get/{postId}")]
        public async Task<IActionResult> Get(string postId)
        {
            JobPost post = await m_JobPostBusiness.Get(postId);
            return Json(post);
        }
        [HttpPost]
        [Route("api/JobPost/getbyactor")]
        public async Task<IActionResult> GetByActor([FromBody] Actor actor)
        {
            List<JobPost> lstStatusPost = await m_JobPostBusiness.GetByActor(actor);
            return Json(lstStatusPost);
        }
        [HttpPost]
        [Route("api/JobPost/getmultiple-JobPost")]
        public async Task<IActionResult> Gets([FromBody] string[] ids)
        {
            List<JobPost> lstStatusPost = await m_JobPostBusiness.Gets(ids);
            return Json(lstStatusPost);
        }
    }
}