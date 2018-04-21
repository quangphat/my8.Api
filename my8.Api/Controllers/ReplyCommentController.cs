using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my8.Api.IBusiness;
using my8.Api.Models;

namespace my8.Api.Controllers
{
    [Produces("application/json")]
    public class ReplyCommentController : Controller
    {
        IReplyCommentBusiness m_ReplyCommentBusiness;
        public ReplyCommentController(IReplyCommentBusiness replycommentBusiness)
        {
            m_ReplyCommentBusiness = replycommentBusiness;
        }
		[HttpPost]
        [Route("api/replycomment/create")]
        public async Task<IActionResult> Create([FromBody] ReplyComment model)
        {
            ReplyComment replycomment= await m_ReplyCommentBusiness.Create(model);
            return Json(replycomment);
        }
        [HttpGet]
        [Route("api/replycomment/getbycomment/{commentId}/{skip}")]
        public async Task<IActionResult> GetByPost(string commentId, int skip)
        {
            List<ReplyComment> replies = await m_ReplyCommentBusiness.GetByComment(commentId, skip);
            return Json(replies);
        }
        [HttpPut]
        [Route("api/replycomment/update")]
        public async Task<IActionResult> Update([FromBody] ReplyComment model)
        {
            bool result = await m_ReplyCommentBusiness.Update(model);
            return Json(result);
        }
        [HttpDelete]
        [Route("api/replycomment/delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool rst = await m_ReplyCommentBusiness.Delete(id);
            return Json(rst);
        }
    }
}