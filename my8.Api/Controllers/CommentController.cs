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
    public class CommentController : Controller
    {
        ICommentBusiness m_CommentBusiness;
        public CommentController(ICommentBusiness commentBusiness)
        {
            m_CommentBusiness = commentBusiness;
        }
		[HttpPost]
        [Route("api/comment/create")]
        public async Task<IActionResult> Create([FromBody] Comment model)
        {
            Comment comment= await m_CommentBusiness.Create(model);
            return Json(comment);
        }
        [HttpGet]
        [Route("api/comment/getbypost/{postId}/{postType}/{skip}")]
        public async Task<IActionResult> GetByPost(string postId,int postType,int skip)
        {
            List<Comment> comments = await m_CommentBusiness.GetByPost(postId, postType,skip);
            return Json(comments);
        }
        [HttpPut]
        [Route("api/comment/update")]
        public async Task<IActionResult> Update([FromBody] Comment model)
        {
            bool result = await m_CommentBusiness.Update(model);
            return Json(result);
        }
        [HttpDelete]
        [Route("api/comment/delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool rst = await m_CommentBusiness.Delete(id);
            return Json(rst);
        }
    }
}