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
    public class PostBroadcastPersonController : BaseController
    {
        IPostBroadcastPersonBusiness m_PostBroadcastPersonBusiness;
        public PostBroadcastPersonController(CurrentProcess process, IPostBroadcastPersonBusiness postbroadcastpersonBusiness):base(process)
        {
            m_PostBroadcastPersonBusiness = postbroadcastpersonBusiness;
        }
        [HttpPost]
        [Route("api/postbroadcast/create")]
        public async Task<IActionResult> BroadcastToPerson([FromBody] StatusPost model)
        {
            bool result = await m_PostBroadcastPersonBusiness.BroadcastToPerson(model);
            return Json(result);
        }
    }
}