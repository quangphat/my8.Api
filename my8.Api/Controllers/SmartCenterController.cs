using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my8.Api.IBusiness;
using my8.Api.Models;
using my8.Api.SmartCenter;

namespace my8.Api.Controllers
{
    [Produces("application/json")]
    public class SmartCenterController : Controller
    {
        ISmartCenter m_SmartCenter;
        public SmartCenterController(ISmartCenter smartCenter)
        {
            m_SmartCenter = smartCenter;
        }
		[HttpPost]
        [Route("api/smartcenter/create")]
        public async Task<IActionResult> Create([FromBody] StatusPost model)
        {
            //model = new JobPost();

            bool result = await m_SmartCenter.BroadcastToPerson(model);
            return Json(result);
        }
    }
}