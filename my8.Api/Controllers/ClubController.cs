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
    public class ClubController : Controller
    {
        MongoI.IClubRepository clubRepositoryM;
        NeoI.IClubRepository clubRepositoryN;

        public ClubController(MongoI.IClubRepository clubRepoM,NeoI.IClubRepository clubRepoN)
        {
            clubRepositoryM = clubRepoM;
            clubRepositoryN = clubRepoN;
        }
    }
}