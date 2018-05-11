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
    public class SeniorityLevelController : BaseController
    {
        ISeniorityLevelBusiness m_SeniorityLevelBusiness;
        public SeniorityLevelController(CurrentProcess process, ISeniorityLevelBusiness senioritylevelBusiness):base(process)
        {
            m_SeniorityLevelBusiness = senioritylevelBusiness;
        }
		[HttpPost]
        [Route("api/senioritylevel/create")]
        public async Task<IActionResult> Create([FromBody] SeniorityLevel model)
        {
            SeniorityLevel senioritylevel= await m_SeniorityLevelBusiness.Create(model);
            return Json(senioritylevel);
        }
        [HttpGet]
        [Route("api/senioritylevel/getall")]
        public async Task<IActionResult> GetAll()
        {
            List<SeniorityLevel> seniorityLevels = await m_SeniorityLevelBusiness.Gets();
            return Json(seniorityLevels);
        }
        [HttpPut]
        [Route("api/senioritylevel/update")]
        public async Task<IActionResult> Update([FromBody] SeniorityLevel model)
        {
            bool result = await m_SeniorityLevelBusiness.Update(model);
            return Json(result);
        }
        [HttpDelete]
        [Route("api/senioritylevel/delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool rst = await m_SeniorityLevelBusiness.Delete(id);
            return Json(rst);
        }
        [HttpGet]
        [Route("api/senioritylevel/search/{searchStr}")]
        public async Task<IActionResult> Search(string searchStr)
        {
            List<SeniorityLevel> seniorityLevels = await m_SeniorityLevelBusiness.Search(searchStr);
            return Json(seniorityLevels);
        }
    }
}