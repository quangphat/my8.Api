using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoM = my8.Api.Models;
using MongoI = my8.Api.Interfaces.Mongo;
using NeoI = my8.Api.Interfaces.Neo4j;
using Model = my8.Api.Models;
using my8.Api.IBusiness;
using my8.Api.Models;

namespace my8.Api.Controllers
{
    [Produces("application/json")]
    public class PersonController : Controller
    {
        IPersonBusiness m_PersonBusiness;
        public PersonController(IPersonBusiness personBusiness)
        {
            m_PersonBusiness = personBusiness;
        }
        [HttpPost]
        [Route("api/person/create")]
        public async Task<IActionResult> CreatePersonMongo([FromBody] Model.Person model)
        {
            Model.Person person =  await m_PersonBusiness.Create(model);
            return Json(person);
        }
        [HttpGet]
        [Route("api/person/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Model.Person person = await m_PersonBusiness.Get(id);
            return Json(person);
        }
        [HttpGet]
        [Route("api/person/GetPersonSql/{id}")]
        public async Task<IActionResult> GetInSqlById(string id)
        {
            Person person = await m_PersonBusiness.GetSql(id);
            return Json(person);
        }
        [HttpPut]
        [Route("api/person/update")]
        public async Task<IActionResult> UpdatePerson([FromBody] Model.Person person)
        {
            bool rs = await m_PersonBusiness.Update(person);
            return Json(rs);
        }
    }
}