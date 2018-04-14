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
    public class ClubController : Controller
    {
        IClubBusiness m_ClubBusiness;
        public ClubController(IClubBusiness clubBusiness)
        {
            m_ClubBusiness = clubBusiness;
        }
        [HttpPost]
        [Route("api/club/create")]
        public async Task<IActionResult> Create([FromBody] Club model)
        {
            Club club = await m_ClubBusiness.Create(model);
            return Json(club);
        }
        [HttpPut]
        [Route("api/club/update")]
        public async Task<IActionResult> Update([FromBody] Club model)
        {
            bool result = await m_ClubBusiness.Update(model);
            return Json(result);
        }
        [HttpGet]
        [Route("api/club/get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            ClubAllin club = await m_ClubBusiness.Get(id);
            return Json(club);
        }
        [HttpGet]
        [Route("api/club/search/{searchStr}/{skip}/{limit}")]
        public async Task<IActionResult> Search(string searchStr, int skip, int limit)
        {
            List<ClubAllin> clubs = await m_ClubBusiness.Search(searchStr, skip, limit);
            return Json(clubs);
        }
        [HttpPost]
        [Route("api/club/addmember/{clubId}/{personId}")]
        public async Task<IActionResult> AddMember(string clubId, string personId)
        {
            bool result = await m_ClubBusiness.AddMember(clubId, personId);
            return Json(result);
        }
        [HttpPost]
        [Route("api/club/kickmember/{clubId}/{personId}")]
        public async Task<IActionResult> KickOutMember(string clubId, string personId)
        {
            bool result = await m_ClubBusiness.KickOutMember(clubId, personId);
            return Json(result);
        }
        [HttpGet]
        [Route("api/club/getmember/{clubId}")]
        public async Task<IActionResult> GetMembers(string clubId)
        {
            List<PersonAllin> lstMembers = await m_ClubBusiness.GetMembers(clubId);
            return Json(lstMembers);
        }
    }
}