using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my8.Api.IBusiness;
using my8.Api.Models;
using my8.Api.SmartCenter;

namespace my8.Api.Controllers
{
    [Produces("application/json")]
    public class FeedController : Controller
    {
        ISmartCenter m_SmartCenter;
        public FeedController(ISmartCenter smart)
        {
            m_SmartCenter = smart;
        }
        [HttpGet]
        [Route("api/Feed/get/{personId}/{skip}")]
        public async Task<IActionResult> Gets(string personId,int skip)
        {
            List<Feed> lstPost = await m_SmartCenter.GetPosts(personId, skip);
            return Json(lstPost);
        }
		//[HttpPost]
  //      [Route("api/Feed/create")]
  //      public async Task<IActionResult> Create([FromBody] Feed model)
  //      {
  //          Feed Feed= await m_FeedBusiness.Create(model);
  //          return Json(Feed);
  //      }
  //      [HttpPut]
  //      [Route("api/Feed/update")]
  //      public async Task<IActionResult> Update([FromBody] Feed model)
  //      {
  //          bool result = await m_FeedBusiness.Update(model);
  //          return Json(result);
  //      }
  //      [HttpDelete]
  //      [Route("api/Feed/delete/{id}")]
  //      public async Task<IActionResult> Delete(string id)
  //      {
  //          bool rst = await m_FeedBusiness.Delete(id);
  //          return Json(rst);
  //      }
    }
}