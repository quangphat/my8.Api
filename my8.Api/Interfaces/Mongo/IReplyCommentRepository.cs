using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IReplyCommentRepository
    {
        Task<string> Create(ReplyComment replycomment);
        Task<ReplyComment> Get(string replyId);
        Task<bool> Update(ReplyComment replycomment);
        Task<bool> Delete(string id);
        Task<List<ReplyComment>> GetByComment(string commentId,int skip,int limit);
    }
}

