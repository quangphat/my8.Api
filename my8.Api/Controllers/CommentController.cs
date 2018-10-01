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
    [Route("comments")]
    public class CommentController : BaseController
    {
        ICommentBusiness m_CommentBusiness;
        public CommentController(CurrentProcess process, ICommentBusiness commentBusiness):base(process)
        {
            m_CommentBusiness = commentBusiness;
        }
		[HttpPost]
        public async Task<IActionResult> Create([FromBody] Comment model)
        {
            Notification commentNotify= await m_CommentBusiness.Create(model);
            return ToResponse(commentNotify);
        }
        [HttpGet]
        [Route("{postId}/{postType}/{skip}")]
        public async Task<IActionResult> GetByPost(string postId,int postType,int skip)
        {
            List<Comment> comments = await m_CommentBusiness.GetByPost(postId, postType,skip);
            return ToResponse(comments);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Comment model)
        {
            bool result = await m_CommentBusiness.Update(model);
            return ToResponse(result);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool rst = await m_CommentBusiness.Delete(id);
            return ToResponse(rst);
        }
    }
}