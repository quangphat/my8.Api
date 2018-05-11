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
    public class IndustryController : BaseController
    {
        IIndustryBusiness m_IndustryBusiness;
        public IndustryController(CurrentProcess process, IIndustryBusiness industryBusiness):base(process)
        {
            m_IndustryBusiness = industryBusiness;
        }
		[HttpPost]
        [Route("api/industry/create")]
        public async Task<IActionResult> Create([FromBody] Industry model)
        {
            Industry industry = await m_IndustryBusiness.Create(model);
            return Json(industry);
        }
        [HttpGet]
        [Route("api/industry/getbyId/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Industry industry = await m_IndustryBusiness.Get(id);
            return Json(industry);
        }
        [HttpGet]
        [Route("api/industry/getbyCode/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            Industry industry = await m_IndustryBusiness.GetByCode(code);
            return Json(industry);
        }
        [HttpGet]
        [Route("api/industry/getall")]
        public async Task<IActionResult> GetAll()
        {
            List<Industry> industries = await m_IndustryBusiness.Gets();
            return Json(industries);
        }
        [HttpPut]
        [Route("api/industry/update")]
        public async Task<IActionResult> Update([FromBody] Industry model)
        {
            bool result = await m_IndustryBusiness.Update(model);
            return Json(result);
        }
        [HttpDelete]
        [Route("api/industry/delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool rst = await m_IndustryBusiness.Delete(id);
            return Json(rst);
        }
        [HttpGet]
        [Route("api/industry/search/{searchStr}")]
        public async Task<IActionResult> Search(string searchStr)
        {
            List<Industry> industries = await m_IndustryBusiness.Search(searchStr);
            return Json(industries);
        }
    }
}