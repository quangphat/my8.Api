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
    public class DistrictController : Controller
    {
        IDistrictBusiness m_DistrictBusiness;
        public DistrictController(IDistrictBusiness districtBusiness)
        {
            m_DistrictBusiness = districtBusiness;
        }
		[HttpPost]
        [Route("api/district/create")]
        public async Task<IActionResult> Create([FromBody] District model)
        {
            District district= await m_DistrictBusiness.Create(model);
            return Json(district);
        }
        [HttpPut]
        [Route("api/district/update")]
        public async Task<IActionResult> Update([FromBody] District model)
        {
            bool result = await m_DistrictBusiness.Update(model);
            return Json(result);
        }
        [HttpDelete]
        [Route("api/district/delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool rst = await m_DistrictBusiness.Delete(id);
            return Json(rst);
        }
    }
}