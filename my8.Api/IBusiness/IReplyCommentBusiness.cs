using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface IReplyCommentBusiness
    {
        Task<ReplyComment> Create(ReplyComment replycomment);
        Task<ReplyComment> Get(string replycommentId);
        Task<bool> Update(ReplyComment replycomment);
        Task<bool> Delete(string id);
        Task<List<ReplyComment>> GetByComment(string commentId,int skip);
    }
}
