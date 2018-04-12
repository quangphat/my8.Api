using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoM = my8.Api.Models;
using MongoI = my8.Api.Interfaces.Mongo;
using NeoI = my8.Api.Interfaces.Neo4j;
using SqlI = my8.Api.Interfaces.Sql;
namespace my8.Api.Controllers
{
    [Produces("application/json")]
    public class ActorTypeController : Controller
    {
        MongoI.IActorTypeRepository actortypeRepositoryM;
        public ActorTypeController(MongoI.IActorTypeRepository actortypeRepoM)
        {
            actortypeRepositoryM = actortypeRepoM;
        }
        [HttpPost]
        [Route("api/m-actortype-create")]
        public async Task CreateActorType([FromBody] MongoM.ActorType actorType)
        {
            await actortypeRepositoryM.Create(actorType);
        }
    }
}