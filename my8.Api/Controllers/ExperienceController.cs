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
    [Route("Experiences")]
    public class ExperienceController : BaseController
    {
        IExperienceBusiness m_ExperienceBusiness;
        public ExperienceController(CurrentProcess process, IExperienceBusiness experienceBusiness) : base(process)
        {
            m_ExperienceBusiness = experienceBusiness;
        }
		[HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] Experience model)
        {
            Experience experience = await m_ExperienceBusiness.Create(model);
            return ToResponse(experience.Id);
        }
        [HttpGet]
        [Route("{experienceId}")]
        public async Task<IActionResult> GetExperience(string experienceId)
        {
            Experience experience = await m_ExperienceBusiness.Get(experienceId);
            return ToResponse(experience);
        }
        [HttpGet]
        [Route("{profileId}/{page}/{limit}")]
        public async Task<IActionResult> GetExperiencesByPerson(string profileId, int page,int limit)
        {
            Pagination<Experience> experiences = await m_ExperienceBusiness.GetByPersonId(profileId,page,limit);
            return ToResponse(experiences);
        }
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] Experience model)
        {
            bool result = await m_ExperienceBusiness.Update(model);
            return ToResponse(result);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool rst = await m_ExperienceBusiness.Delete(id);
            return ToResponse(rst);
        }
    }
}