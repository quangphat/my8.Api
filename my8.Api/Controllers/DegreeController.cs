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
    public class DegreeController : BaseController
    {
        IDegreeBusiness m_DegreeBusiness;
        public DegreeController(CurrentProcess process, IDegreeBusiness degreeBusiness):base(process)
        {
            m_DegreeBusiness = degreeBusiness;
        }
		[HttpPost]
        [Route("api/degree/create")]
        public async Task<IActionResult> Create([FromBody] Degree model)
        {
            Degree degree= await m_DegreeBusiness.Create(model);
            return Json(degree);
        }
        [HttpGet]
        [Route("api/degree/getall")]
        public async Task<IActionResult> GetAll()
        {
            List<Degree> degrees = await m_DegreeBusiness.Gets();
            return Json(degrees);
        }
        [HttpPut]
        [Route("api/degree/update")]
        public async Task<IActionResult> Update([FromBody] Degree model)
        {
            bool result = await m_DegreeBusiness.Update(model);
            return Json(result);
        }
        [HttpDelete]
        [Route("api/degree/delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool rst = await m_DegreeBusiness.Delete(id);
            return Json(rst);
        }
        [HttpGet]
        [Route("api/degree/search/{searchStr}")]
        public async Task<IActionResult> Search(string searchStr)
        {
            List<Degree> degrees = await m_DegreeBusiness.Search(searchStr);
            return Json(degrees);
        }
    }
}