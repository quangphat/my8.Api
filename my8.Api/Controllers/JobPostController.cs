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

namespace my8.Api.Controllers
{
    [Produces("application/json")]
    public class JobPostController : BaseController
    {
        IJobPostBusiness m_JobPostBusiness;
        IPostBroadcastPersonBusiness m_PostBroadCastToPersonBusiness;
        public JobPostController(CurrentProcess process, IJobPostBusiness jobpostBusiness, IPostBroadcastPersonBusiness postBroadcastPersonBusiness):base(process)
        {
            m_JobPostBusiness = jobpostBusiness;
            m_PostBroadCastToPersonBusiness = postBroadcastPersonBusiness;
        }
        [HttpPost]
        [Route("api/JobPost/create")]
        public async Task<IActionResult> CreatePost([FromBody] JobPost model)
        {
            JobPost post = null;
            //bool result = false;
            //model.Active = true;
            //model.PostBy = new Author() { AuthorId = "5ad6c5298895ac2a78afd1ac", DisplayName = "Linh Diệu", AuthorTypeId = (int)AuthorTypeEnum.Person };
            //model.Title = "Tuyển dụng lập trình viên .net";
            //model.Content = "Wayne enterprise cần tuyển .net lương 10k USD/tháng";
            //model.IndustryTags = new List<Industry>()
            //{
            //    new Industry(){Code="it-software", Display="IT-Phần mềm" }
            //};
            //model.SkillTags = new List<Skill>() {
            //    new Skill(){Code=".net", Display=".Net" }
            //};
            post = await m_JobPostBusiness.Post(model);
            return Json(post);
        }
        [HttpGet]
        [Route("api/JobPost/get/{postId}")]
        public async Task<IActionResult> Get(string postId)
        {
            JobPost post = await m_JobPostBusiness.Get(postId);
            return Json(post);
        }
        [HttpPost]
        [Route("api/JobPost/getbyauthor")]
        public async Task<IActionResult> GetByAuthor([FromBody] Author author)
        {
            List<JobPost> lstStatusPost = await m_JobPostBusiness.GetByAuthor(author);
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