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
    public class LocationController : BaseController
    {
        ILocationBusiness m_LocationBusiness;
        public LocationController(CurrentProcess process, ILocationBusiness locationBusiness):base(process)
        {
            m_LocationBusiness = locationBusiness;
        }
		[HttpPost]
        [Route("api/location/create")]
        public async Task<IActionResult> Create([FromBody] Location model)
        {
            Location location= await m_LocationBusiness.Create(model);
            return Json(location);
        }
        [HttpPut]
        [Route("api/location/update")]
        public async Task<IActionResult> Update([FromBody] Location model)
        {
            bool result = await m_LocationBusiness.Update(model);
            return Json(result);
        }
        [HttpDelete]
        [Route("api/location/delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool rst = await m_LocationBusiness.Delete(id);
            return Json(rst);
        }
        [HttpGet]
        [Route("api/location/search/{searchStr}")]
        public async Task<IActionResult> Search(string searchStr)
        {
            List<Location> locations = await m_LocationBusiness.Search(searchStr);
            return Json(locations);
        }
    }
}