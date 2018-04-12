using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my8.Api.IBusiness;

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
    }
}