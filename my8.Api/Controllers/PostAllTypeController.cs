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
    public class PostAllTypeController : Controller
    {
        ISmartCenter m_SmartCenter;
        public PostAllTypeController(ISmartCenter smart)
        {
            m_SmartCenter = smart;
        }
        [HttpGet]
        [Route("api/PostAllType/get/{personId}/{skip}")]
        public async Task<IActionResult> Gets(string personId,int skip)
        {
            List<PostAllType> lstPost = await m_SmartCenter.Gets(personId, skip);
            return Json(lstPost);
        }
		//[HttpPost]
  //      [Route("api/postalltype/create")]
  //      public async Task<IActionResult> Create([FromBody] PostAllType model)
  //      {
  //          PostAllType postalltype= await m_PostAllTypeBusiness.Create(model);
  //          return Json(postalltype);
  //      }
  //      [HttpPut]
  //      [Route("api/postalltype/update")]
  //      public async Task<IActionResult> Update([FromBody] PostAllType model)
  //      {
  //          bool result = await m_PostAllTypeBusiness.Update(model);
  //          return Json(result);
  //      }
  //      [HttpDelete]
  //      [Route("api/postalltype/delete/{id}")]
  //      public async Task<IActionResult> Delete(string id)
  //      {
  //          bool rst = await m_PostAllTypeBusiness.Delete(id);
  //          return Json(rst);
  //      }
    }
}