using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my8.Api.IBusiness;
using my8.Api.Infrastructures;
using my8.Api.Models;

namespace my8.Api.Controllers
{
    [Produces("application/json")]
    [Route("jobFunctions")]
    public class JobFunctionController : BaseController
    {
        IJobFunctionBusiness m_JobFunctionBusiness;
        public JobFunctionController(CurrentProcess process, IJobFunctionBusiness JobFunctionBusiness) : base(process)
        {
            m_JobFunctionBusiness = JobFunctionBusiness;
        }
		[HttpPost]
        public async Task<IActionResult> Create([FromBody] JobFunction model)
        {
            JobFunction JobFunction = await m_JobFunctionBusiness.Create(model);
            return Json(JobFunction);
        }
        [HttpGet]
        [Route("search/{searchStr}")]
        public async Task<IActionResult> Search(string searchStr)
        {
            List<JobFunction> jobFunctions = await m_JobFunctionBusiness.Search(searchStr);
            return ToResponse(jobFunctions);
        }
        [HttpGet]
        [Route("{id}/{code}")]
        public async Task<IActionResult> Get(string id,string code)
        {
            JobFunction JobFunction = null;
            if(!string.IsNullOrWhiteSpace(id))
                JobFunction = await m_JobFunctionBusiness.Get(id);
            else
                JobFunction = await m_JobFunctionBusiness.GetByCode(code);
            return Json(JobFunction);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<JobFunction> results = await m_JobFunctionBusiness.Gets();
            return ToResponse(results);
        }
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] JobFunction model)
        {
            bool result = await m_JobFunctionBusiness.Update(model);
            return Json(result);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool rst = await m_JobFunctionBusiness.Delete(id);
            return Json(rst);
        }
        
    }
}