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
    [Produces("application/json")]
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
        [Route("api/Feed/get/{personId}/{skip}")]
        public async Task<IActionResult> Gets(string personId, int skip)
        {
            List<Feed> lstPost = await m_FeedSmart.GetPosts(personId, skip);
            return ToResponse(lstPost);
        }
        [HttpPost]
        [Route("api/Feed/Test/{personId}")]
        public async Task<IActionResult> CreateBroadcast1MillionPerson(string personId)
        {
            for (int j = 0; j < 1000000; j++)
            {
                //List<Receiver> receivers = new List<Receiver>();
                //for (int i = 0; i < 50000; i++)
                //{
                //    ObjectId receiverId = ObjectId.GenerateNewId();
                //    receivers.Add(new Receiver { PersonId = receiverId.ToString(), Like = false });
                //}
                ObjectId receiverId = ObjectId.GenerateNewId();
                PostBroadcastPerson feed = new PostBroadcastPerson
                {
                    PostId = "",
                    PostType = my8Enum.PostType.StatusPost,
                    ReceiverId = receiverId.ToString()
                };
                var result = await _postBroadcastPersonBusiness.Create(feed);
            }
            //List<Feed> feeds = await m_FeedSmart.GetPosts("5b20901e497a0448b852f34d", 0);
            return ToResponse(false);
        }
        [HttpPost]
        [Route("api/Feed/create/{personId}")]
        public async Task<IActionResult> Create(string personId)
        {
            //Feed Feed = await m_FeedBusiness.Create(model);
            return ToResponse(false);
        }
        [HttpPost]
        [Route("api/Feed/Init/{personId}")]
        public async Task<IActionResult> Init(string personId)
        {
            bool result = await m_FeedSmart.InitBroadcast(personId);
            return ToResponse(result);
        }
        //[HttpPut]
        //[Route("api/Feed/update")]
        //public async Task<IActionResult> Update([FromBody] Feed model)
        //{
        //    bool result = await m_FeedBusiness.Update(model);
        //    return Json(result);
        //}
        //[HttpDelete]
        //[Route("api/Feed/delete/{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    bool rst = await m_FeedBusiness.Delete(id);
        //    return Json(rst);
        //}

    }
}