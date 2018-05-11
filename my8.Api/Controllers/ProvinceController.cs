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
    public class ProvinceController : BaseController
    {
        IProvinceBusiness m_ProvinceBusiness;
        public ProvinceController(CurrentProcess process, IProvinceBusiness provinceBusiness):base(process)
        {
            m_ProvinceBusiness = provinceBusiness;
        }
		[HttpPost]
        [Route("api/province/create")]
        public async Task<IActionResult> Create([FromBody] Province model)
        {
            Province province= await m_ProvinceBusiness.Create(model);
            return Json(province);
        }
        [HttpPut]
        [Route("api/province/update")]
        public async Task<IActionResult> Update([FromBody] Province model)
        {
            bool result = await m_ProvinceBusiness.Update(model);
            return Json(result);
        }
        [HttpDelete]
        [Route("api/province/delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool rst = await m_ProvinceBusiness.Delete(id);
            return Json(rst);
        }
    }
}