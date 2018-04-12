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
    public class UniversityController : Controller
    {
        MongoI.IUniversityRepository universityRepositoryM;
        public UniversityController(MongoI.IUniversityRepository universityRepoM)
        {
            universityRepositoryM = universityRepoM;
        }
    }
}