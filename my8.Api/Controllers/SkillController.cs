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
    public class SkillController : Controller
    {
        ISkillBusiness m_SkillBusiness;
        public SkillController(ISkillBusiness skillBusiness)
        {
            m_SkillBusiness = skillBusiness;
        }
		[HttpPost]
        [Route("api/skill/create")]
        public async Task<IActionResult> Create([FromBody] Skill model)
        {
            model = new Skill();
            model.Code = "hanoi";
            model.Display = "Hà Nội";
            model.KeySearchs = new string[] { "hanoi", "ha noi" };
            Skill skill= await m_SkillBusiness.Create(model);
            return Json(skill);
        }
        [HttpGet]
        [Route("api/skill/getbyId/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Skill skill = await m_SkillBusiness.Get(id);
            return Json(skill);
        }
        [HttpGet]
        [Route("api/skill/getbyCode/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            Skill skill = await m_SkillBusiness.GetByCode(code);
            return Json(skill);
        }
        [HttpGet]
        [Route("api/skill/getAll")]
        public async Task<IActionResult> GetAll()
        {
            List<Skill> skills = await m_SkillBusiness.Gets();
            return Json(skills);
        }
        [HttpPut]
        [Route("api/skill/update")]
        public async Task<IActionResult> Update([FromBody] Skill model)
        {
            bool result = await m_SkillBusiness.Update(model);
            return Json(result);
        }
        [HttpDelete]
        [Route("api/skill/delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool rst = await m_SkillBusiness.Delete(id);
            return Json(rst);
        }
        [HttpGet]
        [Route("api/skill/search/{searchStr}")]
        public async Task<IActionResult> Search(string searchStr)
        {
            List<Skill> industries = await m_SkillBusiness.Search(searchStr);
            return Json(industries);
        }
    }
}