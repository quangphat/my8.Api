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
    [Route("Feed")]
    public class FeedLikeController : BaseController
    {
        IFeedLikeBusiness m_FeedLikeBusiness;
        public FeedLikeController(CurrentProcess process, IFeedLikeBusiness feedlikeBusiness) : base(process)
        {
            m_FeedLikeBusiness = feedlikeBusiness;
        }
		[HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] FeedLike model)
        {
            Notification notify= await m_FeedLikeBusiness.Like(model);
            return ToResponse(notify);
        }
    }
}