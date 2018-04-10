using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoM = my8.Api.Models.Mongo;
using MongoI = my8.Api.Interfaces.Mongo;
using NeoM = my8.Api.Models.Neo4j;
using NeoI = my8.Api.Interfaces.Neo4j;

namespace my8.Api.Controllers
{
    [Produces("application/json")]
    public class PersonController : Controller
    {
        MongoI.IPersonRepository personRepositoryM;
        NeoI.IPersonRepository personRepositoryN;
        public PersonController(MongoI.IPersonRepository personRepoM,NeoI.IPersonRepository personRepoN)
        {
            personRepositoryM = personRepoM;
            personRepositoryN = personRepoN;
        }
        //Mongo
        [HttpPost]
        [Route("api/m-person/create-person")]
        public async Task CreatePersonMongo([FromBody] MongoM.Person model)
        {
            await personRepositoryM.Create(model);
        }
        [HttpGet]
        [Route("api/m-person/{id}")]
        public async Task<IActionResult> GetPersonById(string id)
        {
            MongoM.Person Person = await personRepositoryM.Get(id);
            return Json(Person);
        }
        [HttpPut]
        [Route("api/m-person/update")]
        public async Task UpdatePerson([FromBody] MongoM.Person person)
        {
            await personRepositoryM.Update(person);
        }
        //Neo4j
        [HttpPost]
        [Route("api/n-person/interactiontofriend")]
        public async Task CreatePersonNeo([FromBody] List<NeoM.Person> people)
        {
            if(people!=null)
            {
                NeoM.Person current = people[0];
                NeoM.Person friend = people[1];
                await personRepositoryN.InteractionToFriend(current, friend);
            }
            
        }
    }
}