using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface ICommentBusiness
    {
        Task<CommentNotify> Create(Comment comment);
        Task<Comment> Get(string commentId);
        Task<List<Comment>> GetByPost(string postId,int postType,int skip);
        Task<bool> Update(Comment comment);
        Task<bool> Delete(string id);
    }
}
