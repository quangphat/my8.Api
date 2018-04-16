using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my8.Api.IBusiness;
using my8.Api.Models;

namespace my8.Api.Controllers
{
    [Produces("application/json")]
    public class JobPostController : Controller
    {
        IJobPostBusiness m_JobPostBusiness;
        public JobPostController(IJobPostBusiness jobpostBusiness)
        {
            m_JobPostBusiness = jobpostBusiness;
        }
		[HttpPost]
        [Route("api/jobpost/create")]
        public async Task<IActionResult> Create([FromBody] JobPost model)
        {
            JobPost jobpost= await m_JobPostBusiness.Create(model);
            return Json(jobpost);
        }
        [HttpPut]
        [Route("api/jobpost/update")]
        public async Task<IActionResult> Update([FromBody] JobPost model)
        {
            bool result = await m_JobPostBusiness.Update(model);
            return Json(result);
        }
        [HttpDelete]
        [Route("api/jobpost/delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool rst = await m_JobPostBusiness.Delete(id);
            return Json(rst);
        }
    }
}