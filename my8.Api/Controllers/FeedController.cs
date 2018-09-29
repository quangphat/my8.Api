using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using my8.Api.IBusiness;
using my8.Api.Infrastructures;
using my8.Api.Interfaces.Mongo;
using my8.Api.ISmartCenter;
using my8.Api.Models;

namespace my8.Api.Controllers
{
    [Route("Feeds")]
    public class FeedController : BaseController
    {
        IFeedSmart m_FeedSmart;
        IPostBroadcastPersonRepository _postBroadcastPersonBusiness;
        public FeedController(CurrentProcess process, IFeedSmart feedSmart, IPostBroadcastPersonRepository postBroadcastPersonBusiness) : base(process)
        {
            m_FeedSmart = feedSmart;
            _postBroadcastPersonBusiness = postBroadcastPersonBusiness;
        }
        [HttpGet]
        [Route("{personId}/{skip}")]
        public async Task<IActionResult> Gets(string personId, int skip)
        {
            List<Feed> lstPost = await m_FeedSmart.GetPosts(personId, skip);
            return ToResponse(lstPost);
        }
        [HttpPost]
        [Route("{personId}/Init")]
        public async Task<IActionResult> Init(string personId)
        {
            bool result = await m_FeedSmart.InitBroadcast(personId);
            return ToResponse(result);
        }

    }
}