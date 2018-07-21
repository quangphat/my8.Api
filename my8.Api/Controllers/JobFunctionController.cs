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
    public class JobFunctionController : BaseController
    {
        IJobFunctionBusiness m_JobFunctionBusiness;
        public JobFunctionController(CurrentProcess process, IJobFunctionBusiness JobFunctionBusiness) : base(process)
        {
            m_JobFunctionBusiness = JobFunctionBusiness;
        }
		[HttpPost]
        [Route("api/JobFunction/create")]
        public async Task<IActionResult> Create([FromBody] JobFunction model)
        {
            JobFunction JobFunction = await m_JobFunctionBusiness.Create(model);
            return Json(JobFunction);
        }
        [HttpGet]
        [Route("api/JobFunction/getbyId/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            JobFunction JobFunction = await m_JobFunctionBusiness.Get(id);
            return Json(JobFunction);
        }
        [HttpGet]
        [Route("api/JobFunction/getbyCode/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            JobFunction JobFunction = await m_JobFunctionBusiness.GetByCode(code);
            return Json(JobFunction);
        }
        [HttpGet]
        [Route("api/JobFunction")]
        public async Task<IActionResult> GetAll()
        {
            List<JobFunction> results = await m_JobFunctionBusiness.Gets();
            return ToResponse(results);
        }
        [HttpPut]
        [Route("api/JobFunction/update")]
        public async Task<IActionResult> Update([FromBody] JobFunction model)
        {
            bool result = await m_JobFunctionBusiness.Update(model);
            return Json(result);
        }
        [HttpDelete]
        [Route("api/JobFunction/delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool rst = await m_JobFunctionBusiness.Delete(id);
            return Json(rst);
        }
        [HttpGet]
        [Route("api/JobFunction/search/{searchStr}")]
        public async Task<IActionResult> Search(string searchStr)
        {
            List<JobFunction> jobFunctions = await m_JobFunctionBusiness.Search(searchStr);
            return ToResponse(jobFunctions);
        }
    }
}