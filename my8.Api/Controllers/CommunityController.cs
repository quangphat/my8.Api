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
    public class CommunityController : BaseController
    {
        ICommunityBusiness m_CommunityBusiness;
        public CommunityController(CurrentProcess process, ICommunityBusiness CommunityBusiness) : base(process)
        {
            m_CommunityBusiness = CommunityBusiness;
        }
        [HttpPost]
        [Route("api/Community/create")]
        public async Task<IActionResult> Create([FromBody] Community model)
        {
            Community Community = await m_CommunityBusiness.Create(model);
            return Json(Community);
        }
        [HttpPut]
        [Route("api/Community/update")]
        public async Task<IActionResult> Update([FromBody] Community model)
        {
            bool result = await m_CommunityBusiness.Update(model);
            return Json(result);
        }
        [HttpGet]
        [Route("api/Community/get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            CommunityAllin Community = await m_CommunityBusiness.Get(id);
            return Json(Community);
        }
        [HttpGet]
        [Route("api/Community/search/{searchStr}/{skip}/{limit}")]
        public async Task<IActionResult> Search(string searchStr, int skip, int limit)
        {
            List<CommunityAllin> Communitys = await m_CommunityBusiness.Search(searchStr, skip, limit);
            return Json(Communitys);
        }
        [HttpPost]
        [Route("api/Community/addmember/{CommunityId}/{personId}")]
        public async Task<IActionResult> AddMember(string CommunityId, string personId)
        {
            bool result = await m_CommunityBusiness.AddMember(CommunityId, personId);
            return Json(result);
        }
        [HttpPost]
        [Route("api/Community/kickmember/{CommunityId}/{personId}")]
        public async Task<IActionResult> KickOutMember(string CommunityId, string personId)
        {
            bool result = await m_CommunityBusiness.KickOutMember(CommunityId, personId);
            return Json(result);
        }
        [HttpGet]
        [Route("api/Community/getmember/{CommunityId}")]
        public async Task<IActionResult> GetMembers(string CommunityId)
        {
            List<PersonAllin> lstMembers = await m_CommunityBusiness.GetMembers(CommunityId);
            return Json(lstMembers);
        }
    }
}